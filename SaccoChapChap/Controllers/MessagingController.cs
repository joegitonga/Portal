using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaccoChapChap.Models;
using SaccoChapChap.Services;
using System.Web.Security;
using Dapper;
using System.IO;
using System.Text;
using LinqToExcel;

namespace SaccoChapChap.Controllers
{
    public class MessagingController : Controller
    {
        System.Data.IDbConnection _db = SaccoChapChap.Services.DAL.DapperConnection();
        DAL dbService = new DAL();

        public ActionResult AdHoc()
        {   
            ViewData["title"] = "AdHoc SMS";
            return View();
        }

        [HttpPost]
        public ActionResult AdHoc(AdhocSMSModel model)
        {
            model.BankID = System.Configuration.ConfigurationManager.AppSettings["SmsBankID"].ToString();

            if (ModelState.IsValid)
            {
                try
                {
                    string[] values = model.PhoneNumber.Split(',');

                    foreach (string thePhone in values)
                    {
                        var userlist = _db.Query<string>(";Exec InsertSmsListing @BankID,@PhoneNumber,@Description",
                            new
                            {
                                BankID = model.BankID,
                                PhoneNumber = thePhone,
                                Description = model.Description
                            }).SingleOrDefault();
                    }



                    TempData["Success"] = "Adhoc SMS qeued Successfuly!";
                    return RedirectToAction("AdHoc");
                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.Message;
                    return View("AdHoc", model);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ViewData["Error"] = message;
                return View("AdHoc", model);
            }
        }

        public ActionResult UploadExcelFile()
        {
            ViewData["title"] = "Upload Excel File";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadExcelFile(ExcelUploadModel uploadExcelModel, HttpPostedFileBase file)
        {            
            if (ModelState.IsValid)
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/App_data"), fileName);
                    file.SaveAs(path);
                    try
                    {                       
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("<?xml version=\"1.0\" ?>");
                            var excelFile = new ExcelQueryFactory(Convert.ToString(path));
                            var getData = from a in excelFile.Worksheet("Sheet1") select a;
                            foreach (var a in getData)
                            {
                                uploadExcelModel.AccountID = a["AccountID"];
                                uploadExcelModel.PhoneNumber = a["PhoneNumber"];
                                sb.AppendLine("<ExcelUpload>");
                                sb.AppendLine(" <SendMessage>");
                                sb.AppendLine("  <AccountID>" + uploadExcelModel.AccountID + "</AccountID>");
                                sb.AppendLine("  <Description>" + uploadExcelModel.Description + "</Description>");
                                sb.AppendLine("  <filePath>" + Convert.ToString(path)+ "</filePath>");
                                sb.AppendLine("  <PhoneNumber>" + uploadExcelModel.PhoneNumber + "</PhoneNumber>");
                                sb.AppendLine(" </SendMessage>");
                                sb.AppendLine("</ExcelUpload>");

                            }
                        var excelUploadQuery = _db.Query<string>(";Exec P_AddNewExcelUpload @ExcelUploadFile",
                                new
                                {
                                    ExcelUploadFile = sb.ToString()
                                }).SingleOrDefault();

                        TempData["Success"] = "Excel File qeued Successfuly!";
                        return RedirectToAction("UploadExcelFile");
                    }
                    catch(Exception e)
                    {
                        ViewData["Error"] = e.Message;
                        return View("UploadExcelFile", uploadExcelModel);
                    }
                }
                else
                {
                    ViewData["Error"] = "Excel File is Invalid. Try Again!";
                    return RedirectToAction("UploadExcelFile");
                }

            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage));

                ViewData["Error"] =message;
                return View("UploadExcelFile", uploadExcelModel);
            }
        }

        public ActionResult Disbursement()
        {
            ViewData["title"] = "Dusbursement SMS";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult ActiveLoans(GeneralModel model)
        {
            try
            {
                var RecCount = 0;
                List<LoanListModel> loanlist = new List<LoanListModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchActiveLoanss @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    loanlist = multi.Read<LoanListModel>().ToList();
                }
                return Json(new { Result = "OK", Records = loanlist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult UpdateDisbursementSMS(LoanListModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userlist = _db.Query<string>(";Exec UpdateDisbursement @UniqueID,@MemberNo,@ChequeID,@Remarks,@SentBy",
                        new
                        {
                            UniqueID = model.UniqueID,
                            MemberNo = model.MemberNo,
                            ChequeID = model.ChequeID,
                            Remarks = model.Remarks,
                            SentBy = User.Identity.Name
                        }).SingleOrDefault();

                    return Json(new { Result = "OK" });
                    //return Json(new { Result = "OK", Record = userlist });
                }
                catch (Exception e)
                {
                    return Json(new { Result = "ERROR", Message = e.Message });
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Json(new { Result = "ERROR", Message = message });
            }
        }

        public ActionResult SmsListing()
        {
            ViewData["title"] = "SMS Listing";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SmsListing(ListingModel model)
        {
            try
            {
                var RecCount = 0;
                List<SmsListingModel> loanlist = new List<SmsListingModel>();

                using (var multi = _db.QueryMultiple(";Exec p_FetchSentSms @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStart,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    loanlist = multi.Read<SmsListingModel>().ToList();
                }
                return Json(new { Result = "OK", Records = loanlist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult Arrears()
        {
            ViewData["title"] = "Arrears SMS";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult ArrearsListing(ArrearsModel model)
        {
            try
            {
                var RecCount = 0;
                List<ArrearsListModel> arrearslist = new List<ArrearsListModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchArrearsLoans @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    arrearslist = multi.Read<ArrearsListModel>().ToList();
                }
                return Json(new { Result = "OK", Records = arrearslist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult UpdateArrearsSMS(ArrearsListModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userlist = _db.Query<string>(";Exec UpdateArrearsSmsDetails @UniqueID,@MemberNo,@ChequeID,@Remarks,@SentBy",
                        new
                        {
                            UniqueID = model.UniqueID,
                            MemberNo = model.MemberNo,
                            ChequeID = model.ChequeID,
                            Remarks = model.Remarks,
                            SentBy = User.Identity.Name
                        }).SingleOrDefault();

                    return Json(new { Result = "OK" });
                    //return Json(new { Result = "OK", Record = userlist });
                }
                catch (Exception e)
                {
                    return Json(new { Result = "ERROR", Message = e.Message });
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Json(new { Result = "ERROR", Message = message });
            }
        }

        public ActionResult Pdc()
        {
            ViewData["title"] = "Pdc Processing";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult PdChequesListing(GeneralModel model)
        {
            try
            {
                var RecCount = 0;
                List<PdcSmsDetailsModel> loanlist = new List<PdcSmsDetailsModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchActivePdc @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    loanlist = multi.Read<PdcSmsDetailsModel>().ToList();
                }
                return Json(new { Result = "OK", Records = loanlist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult PdChequePosting(PdcSmsDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userlist = _db.Query<string>(";Exec UpdatePdc @UniqueID,@MemberNo,@ChequeID,@Remarks,@SentBy",
                        new
                        {
                            UniqueID = model.UniqueID,
                            MemberNo = model.MemberNo,
                            ChequeID = model.ChequeID,
                            Remarks = model.Remarks,
                            SentBy = User.Identity.Name
                        }).SingleOrDefault();

                    return Json(new { Result = "OK" });
                    //return Json(new { Result = "OK", Record = userlist });
                }
                catch (Exception e)
                {
                    return Json(new { Result = "ERROR", Message = e.Message });
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Json(new { Result = "ERROR", Message = message });
            }
        }
    }
}
