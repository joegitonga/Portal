using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaccoChapChap.Models;
using System.Web.Security;
using Dapper;

namespace SaccoChapChap.Controllers
{
    public class CustomerController : Controller
    {
        System.Data.IDbConnection _db = SaccoChapChap.Services.DAL.DapperConnection();
        //
        // GET: /Customer/

        [Authorize]
        public ActionResult MaintainClient()
        {
            ViewData["title"] = "Client Maintenance";
            return View();
        }

        [Authorize]
        public ActionResult ClientRegister()
        {
            ViewData["title"] = "Client Registration";
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult RegisterClient(RegisterClientModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Random r = new Random();
                    int randNum = r.Next(10000);
                    string sixDigitPin = randNum.ToString("D4");
                    string encodePin = SaccoChapChap.Services.DAL.EncodePassword(sixDigitPin);
                    
                    string strCheckSUM = string.Concat(model.ClientID, model.FirstName,model.OtherNames);
                    strCheckSUM = string.Concat(strCheckSUM, model.Email != null ? model.Email.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, model.Mobile, model.Remarks != null ? model.Remarks.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, User.Identity.Name);

                    string strCheckSumHash = SaccoChapChap.Services.DAL.GetCheckSum(strCheckSUM);

                    List<ClientListModel> userlist = _db.Query<ClientListModel>(";Exec CreateClient @ClientID,@FirstName,@OtherNames,@Password,@Email,@Mobile,@Remarks,@CreatedBy,@CheckSumID",
                        new
                        {
                            ClientID = model.ClientID,
                            FirstName = model.FirstName,
                            OtherNames = model.OtherNames,
                            Password = encodePin,
                            Email = model.Email,
                            Mobile = model.Mobile,
                            Remarks = model.Remarks,
                            CreatedBy = User.Identity.Name,
                            CheckSumID = strCheckSumHash
                        }).ToList();

                    TempData["Success"] = "Client Saved Successfuly!";
                    return RedirectToAction("ClientRegister");

                    //return Json(new { Result = "OK", Record = userlist});
                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.Message;
                    return View("ClientRegister",model);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ViewData["Error"] = message;
                return View("ClientRegister", model);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult UpdateClientRegister(UpdateClientRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string strCheckSUM = string.Concat(model.ClientID, model.FirstName, model.OtherNames);
                    strCheckSUM = string.Concat(strCheckSUM, model.Email != null ? model.Email.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, model.Mobile, model.Remarks != null ? model.Remarks.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, User.Identity.Name);

                    string strCheckSumHash = SaccoChapChap.Services.DAL.GetCheckSum(strCheckSUM);

                    List<ClientListModel> userlist = _db.Query<ClientListModel>(";Exec UpdateClient @UniqueID,@ClientID,@FirstName,@OtherNames,@Email,@Mobile,@Remarks,@CreatedBy,@CheckSumID",
                        new
                        {
	                        UniqueID	= model.UniqueID,
                            ClientID    = model.ClientID,
	                        FirstName	= model.FirstName,
	                        OtherNames	= model.OtherNames,
	                        Email		= model.Email,
	                        Mobile		= model.Mobile,
                            Remarks     = model.Remarks,
                            CreatedBy   = User.Identity.Name,
                            CheckSumID  = strCheckSumHash
                        }).ToList();

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

        [HttpPost]
        [Authorize]
        public JsonResult ActiveClients(GeneralModel model)
        {
            try
            {
                var RecCount = 0;
                List<ClientListModel> userlist = new List<ClientListModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchActiveClients @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    userlist = multi.Read<ClientListModel>().ToList();
                }
                return Json(new { Result = "OK", Records = userlist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult UnsupervisedClients(GeneralModel model)
        {
            try
            {
                var RecCount = 0;
                List<ClientListModel> userlist = new List<ClientListModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchUnsupervisedClients @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    userlist = multi.Read<ClientListModel>().ToList();
                }
                return Json(new { Result = "OK", Records = userlist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult ClientPINChange(ClientPINChangeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Random r = new Random();
                    int randNum = r.Next(10000);
                    string sixDigitPin = randNum.ToString("D4");
                    string encodePin = SaccoChapChap.Services.DAL.EncodePassword(sixDigitPin);

                    List<ClientPINChangeModel> Pintlist = _db.Query<ClientPINChangeModel>(";Exec PINChange @UniqueID,@Password,@Remarks,@CreatedBy",
                        new
                        {
                            UniqueID = model.UniqueID,
                            Password = encodePin,
                            Remarks = model.Remarks,
                            CreatedBy = User.Identity.Name
                        }).ToList();

                    //return Json(new { Result = "OK" });
                    return Json(new { Result = "OK", Record = Pintlist });
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

        [HttpPost]
        [Authorize]
        public JsonResult FetchPassHistory(PassHistoryModel model)
        {
            try
            {
                var RecCount = 0;
                List<GridPassModel> passhistlist = new List<GridPassModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchPasswordHistory @jtStartIndex,@jtPageSize,@PasswordType,@UniqueID",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize,
                        PasswordType    = model.PasswordType,
                        UniqueID = model.UniqueID
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    passhistlist = multi.Read<GridPassModel>().ToList();
                }
                return Json(new { Result = "OK", Records = passhistlist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [Authorize]
        public ActionResult ApproveClient()
        {
            ViewData["title"] = "Client Supervision";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult ApproveClient(UpdateClientRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string strCheckSUM = string.Concat(model.ClientID, model.FirstName, model.OtherNames);
                    strCheckSUM = string.Concat(strCheckSUM, model.Email != null ? model.Email.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, model.Mobile, model.Remarks != null ? model.Remarks.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, User.Identity.Name);

                    string strCheckSumHash = SaccoChapChap.Services.DAL.GetCheckSum(strCheckSUM);

                    List<ClientListModel> userlist = _db.Query<ClientListModel>(";Exec ApproveClient @UniqueID,@ClientID,@Remarks,@CreatedBy,@CheckSumID",
                        new
                        {
                            UniqueID = model.UniqueID,
                            ClientID = model.ClientID,
                            Remarks = model.Remarks,
                            CreatedBy = User.Identity.Name,
                            CheckSumID = strCheckSumHash
                        }).ToList();

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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
        }
    }
}
