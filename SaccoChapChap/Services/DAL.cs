using SaccoChapChap.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using Dapper;

namespace SaccoChapChap.Services
{
    public class DAL
    {
        public static IDbConnection DapperConnection()
        {
            IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
            return _db;
        }

        public static bool ValidateUser(string username, string password)
        {
            bool isValidUser = false;
            string _Password = string.Empty;

            try
            {
                _Password = EncodePassword(password);
                System.Data.IDbConnection _db = DapperConnection();
                List<userListModel> trxs = _db.Query<userListModel>(";Exec AuthenticateUser @UserName, @Password",
                    new
                    {
                        UserName = username,
                        Password = _Password
                    }).ToList();

                //List<User> trxs = con.Fetch<User>(";Exec getAuthenticateUser @UserName, @Password", new { UserName= username, Password= _Password });

                isValidUser = trxs[0].UserName.ToString().ToUpper().Trim().Equals(username.ToUpper());
                FormsAuthentication.SetAuthCookie(trxs[0].UserName.ToString().ToUpper(), false);
            }
            catch (Exception ee)
            {

            }

            return isValidUser;
        }

        public static string EncodePassword(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static String GetCheckSum(string value)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] retVal = sha1.ComputeHash(Encoding.Default.GetBytes(value));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public IEnumerable<ModuleAccess> GetModuleAccessRights(int _UserID)
        {
            System.Data.IDbConnection _db = DapperConnection();

            List<ModuleAccess> modulerights =
                _db.Query<ModuleAccess>(";Exec getUserMainMenu @UserName", new
                {
                    UserName = Membership.GetUser().UserName.ToString()
                }).ToList();

            return modulerights;
        }

        public IEnumerable<breadCrumbsModel> GetBreadCrumb(int thePage)
        {
            List<breadCrumbsModel> hReturn = new List<breadCrumbsModel>();
            switch (thePage)
            {
                case 1:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "Security", action = "New User" } };
                    break;
                case 2:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "Registration", action = "New Client" } };
                    break;
                case 3:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "Registration", action = "Client Approval" } };
                    break;
                case 4:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "Registration", action = "Client Management" } };
                    break;
                case 5:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "Security", action = "User Supervision" } };
                    break;
                case 6:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "SMS Manager", action = "AdHoc SMS" } };
                    break;
                case 7:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "SMS Manager", action = "Excel Upload SMS" } };
                    break;
                case 8:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "SMS Manager", action = "Disbursement SMS" } };
                    break;
                case 9:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "SMS Manager", action = "Arrears Notifications" } };
                    break;
                case 10:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "SMS Manager", action = "SMS Listing" } };
                    break;
                case 11:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "SMS Manager", action = "PDC Processing" } };
                    break;
                case 12:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "Sales Manager", action = "Sales Listing" } };
                    break;
                default:
                    hReturn = new List<breadCrumbsModel> { new breadCrumbsModel { controller = "", action = "" } };
                    break;
            }

            return hReturn;
        }

    }
}