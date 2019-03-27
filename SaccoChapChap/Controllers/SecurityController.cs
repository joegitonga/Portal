using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaccoChapChap.Models;
using SaccoChapChap.Services;
using System.Web.Security;
using Dapper;

namespace SaccoChapChap.Controllers
{
    public class SecurityController : Controller
    {
        System.Data.IDbConnection _db = SaccoChapChap.Services.DAL.DapperConnection();
        DAL dbService = new DAL();

        [Authorize]
        public ActionResult MaintainUser()
        {
            ViewData["title"] = "User maintenance";
            return View();
        }
        
        [Authorize]
        public ActionResult CreateUser()
        {
            ViewData["title"] = "User Creation";
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult RegisterUser(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string encodePassword = SaccoChapChap.Services.DAL.EncodePassword(model.Password);
                    string strCheckSumHash = SaccoChapChap.Services.DAL.GetCheckSum(model.Password);

                    _db.Query(";Exec CreateUser @UserName,@Password,@FirstName,@OtherNames,@Email,@Mobile,@CreatedBy",
                        new
                        {
                            UserName = model.UserName,
                            Password = encodePassword,
                            FirstName = model.FirstName,
                            OtherNames = model.OtherNames,
                            Email = model.Email,
                            Mobile = model.Mobile,
                            CreatedBy = User.Identity.Name
                        }).SingleOrDefault();

                    TempData["Success"] = "User Saved Successfuly!";
                    return RedirectToAction("CreateUser");
                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.Message;
                    return View("CreateUser", model);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ViewData["Error"] = message;
                return View("CreateUser", model);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult ModifyUser(UpdateRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Query(";Exec UpdateUser @UserID,@FirstName,@OtherNames,@Email,@Mobile",
                        new
                        {
                            UserID = model.UserID,
                            FirstName = model.FirstName,
                            OtherNames = model.OtherNames,
                            Email = model.Email,
                            Mobile = model.Mobile
                        }).SingleOrDefault();

                    return Json(new { Result = "OK" });
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
        public JsonResult ListUsers(GeneralModel model)
        {
            try
            {
                var RecCount = 0;
                List<userListModel> userlist = new List<userListModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchActiveUsers @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    userlist = multi.Read<userListModel>().ToList();
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
        public JsonResult UnsupervisedUsers(GeneralModel model)
        {
            try
            {
                var RecCount = 0;
                List<userListModel> userlist = new List<userListModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchUnsupervisedUsers @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    userlist = multi.Read<userListModel>().ToList();
                }
                return Json(new { Result = "OK", Records = userlist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [Authorize]
        public ActionResult ApproveUser()
        {
            ViewData["title"] = "User Supervision";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SuperviseUser(UpdateRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    string strCheckSUM = string.Concat(model.UserID, model.FirstName, model.OtherNames);
                    strCheckSUM = string.Concat(strCheckSUM, model.Email != null ? model.Email.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, model.Mobile, model.Remarks != null ? model.Remarks.ToString() : String.Empty);
                    strCheckSUM = string.Concat(strCheckSUM, User.Identity.Name);

                    string strCheckSumHash = SaccoChapChap.Services.DAL.GetCheckSum(strCheckSUM);

                    List<ClientListModel> userlist = _db.Query<ClientListModel>(";Exec SuperviseUser @UserID,@Remarks,@CreatedBy,@CheckSumID",
                        new
                        {
                            UserID = model.UserID,
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
    }
}
