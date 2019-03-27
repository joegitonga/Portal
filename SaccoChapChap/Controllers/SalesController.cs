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
    public class SalesController : Controller
    {
        System.Data.IDbConnection _db = SaccoChapChap.Services.DAL.DapperConnection();
        DAL dbService = new DAL();

        public ActionResult SalesListing()
        {
            ViewData["title"] = "Sales";
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SalesListing(GeneralModel model)
        {
            try
            {
                var RecCount = 0;
                List<SalesModel> arrearslist = new List<SalesModel>();

                using (var multi = _db.QueryMultiple(";Exec FetchSalesMade @jtStartIndex,@jtPageSize",
                    new
                    {
                        jtStartIndex = model.jtStartIndex,
                        jtPageSize = model.jtPageSize
                    }))
                {
                    RecCount = multi.Read<Int32>().Single();
                    arrearslist = multi.Read<SalesModel>().ToList();
                }
                return Json(new { Result = "OK", Records = arrearslist, TotalRecordCount = RecCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
