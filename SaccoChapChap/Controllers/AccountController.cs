using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaccoChapChap.Models;
using SaccoChapChap.Services;
using System.Web.Security;
using Dapper;
using WebMatrix.WebData;

namespace SaccoChapChap.Controllers
{
    public class AccountController : Controller
    {
        System.Data.IDbConnection _db = SaccoChapChap.Services.DAL.DapperConnection();
        DAL dbService = new DAL();

        [ChildActionOnly]
        [Authorize]
        public ActionResult ModuleAccess()
        {
            UserAccessRights AccessModel = new UserAccessRights();

            AccessModel.moduleaccess = dbService.GetModuleAccessRights(int.Parse(Membership.GetUser().ProviderUserKey.ToString()));

            return View("~/Views/Shared/_MainMenu.aspx", AccessModel);
        }

        [ChildActionOnly]
        [Authorize]
        public ActionResult BreadCrumb(int PageCode)
        {
            breadCrumbModel BreadModel = new breadCrumbModel();

            BreadModel.breadcrumb = dbService.GetBreadCrumb(PageCode);

            return View("~/Views/Shared/_breadCrumb.aspx", BreadModel);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewData["title"] = "Login Authentication";
            string returnUrl = string.Empty;
            Session.Abandon();
            Session.Clear();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    return RedirectToLocal(returnUrl);
                }
                catch (Exception e)
                {
                    return View(model);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Authorize]
        public JsonResult UserPasswordChange(UserPasswordChangeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string sixAutomaticPassword = SaccoChapChap.Services.DAL.RandomString(6);
                    string encodePassword= SaccoChapChap.Services.DAL.EncodePassword(sixAutomaticPassword);

                    List<UserPasswordChangeModel> Pintlist = _db.Query<UserPasswordChangeModel>(";Exec PasswordChange @UserID,@Password,@Remarks,@CreatedBy",
                        new
                        {
                            UserID = model.UserID,
                            Password = encodePassword,
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }   
        }
    }
}
