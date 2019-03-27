using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Reflection;
using Dapper;

namespace SaccoChapChap.Models
{

    public class User
    {
        public Int32 UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }

    class CustomMembershipProvider : MembershipProvider
    {
        private int _minRequiredPasswordLength;
        System.Data.IDbConnection _db = SaccoChapChap.Services.DAL.DapperConnection();

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        //User authentication. returns true or false
        public override bool ValidateUser(string username, string password)
        {
            bool isValidUser = false;
            string _Password = string.Empty;

            try
            {
                _Password = EncodePassword(password);
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateNewUser(string username, string password, string firstname, string lastname, string email, string mobile, int roleID, string createdby, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs Args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(Args);

            if (Args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            try
            {
                MembershipUser user = GetUser(username, false);
                bool isApproved = true;
                if (user == null)
                {
                    string strHashedPassword = EncodePassword(password);

                    int trxs = _db.Query<int>(";Exec AddUser @UserName,@Password,@Email,@Firstname,@Lastname,@Mobile,@isApproved,@CreatedBy,@RoleID",
                        new
                        {
                            UserName = username.ToUpper(),
                            Password = EncodePassword(password),
                            Email = email,
                            Firstname = firstname,
                            Lastname = lastname,
                            Mobile = mobile,
                            isApproved = isApproved,
                            CreatedBy = createdby,
                            RoleID = roleID
                        }).SingleOrDefault();

                    status = MembershipCreateStatus.Success;

                    return GetUser(username, true);
                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
            }
            catch
            {
                status = MembershipCreateStatus.UserRejected;
            }

            return null;
        }


        public MembershipUser UpdateUser(string username, string firstname, string lastname, string email, string mobile, int roleID, string createdby, out MembershipCreateStatus status)
        {
            try
            {
                MembershipUser user = GetUser(username, false);
                if (user != null)
                {

                    int trxs = _db.Query<int>(";Exec UpdateUser @UserName,@Email,@Firstname,@Lastname,@Mobile,@CreatedBy,@RoleID",
                        new
                        {
                            UserName = username.ToUpper(),
                            Email = email,
                            Firstname = firstname,
                            Lastname = lastname,
                            Mobile = mobile,
                            CreatedBy = createdby,
                            RoleID = roleID
                        }).SingleOrDefault();

                    status = MembershipCreateStatus.Success;
                    return GetUser(username, true);
                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
            }
            catch
            {
                status = MembershipCreateStatus.UserRejected;
            }

            return null;
        }


        public MembershipUser UpdatePassword(int userID, string username, string password, string createdby, out MembershipCreateStatus status)
        {
            try
            {
                MembershipUser user = GetUser(username, false);
                if (user != null)
                {
                    int trxs = _db.Query<int>(";Exec UpdatePassword @UserName, @Password,@CreatedBy",
                        new
                        {
                            UserName = username.ToUpper(),
                            Password = EncodePassword(password),
                            CreatedBy = createdby
                        }).SingleOrDefault();


                    status = MembershipCreateStatus.Success;
                    return GetUser(username, true);
                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
            }
            catch
            {
                status = MembershipCreateStatus.UserRejected;
            }

            return null;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs Args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(Args);

            if (Args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            ///We can use this to check for duplicate email address
            //if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            //{
            //    status = MembershipCreateStatus.DuplicateEmail;
            //    return null;
            //}

            try
            {
                MembershipUser user = GetUser(username, false);

                if (user == null)
                {
                    string strHashedPassword = EncodePassword(password);
                    //int recAdded = dbAccessORM.ExecuteSave("AddUser", new { UserName = username.ToUpper(), Password = EncodePassword(password), Email = email, isApproved = isApproved });

                    //int recAdded = con.Execute(";Exec CreateUser @UserName,@Password",
                    //    new { username, strHashedPassword});

                    status = MembershipCreateStatus.Success;

                    return GetUser(username, true);
                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
            }
            catch
            {
                status = MembershipCreateStatus.UserRejected;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser u = null;
            try
            {
                List<userListModel> UsrDtls = _db.Query<userListModel>(";Exec getUserWithUserName @UserName",
                    new
                    {
                        UserName = username
                    }).ToList();

                DataTable dt = ToDataTable(UsrDtls);//dbAccessORM.ExecuteRD("getUserWithUserName", new { UserName = username });

                u = GetUserFromDataTable(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return u;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return 6;
            }

        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
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

        private MembershipUser GetUserFromDataTable(DataTable dtable)
        {
            DataView dv = new DataView(dtable);

            object providerUserKey = dv[0]["UserID"];
            string username = dv[0]["UserName"].ToString();
            string email = dv[0]["Email"].ToString();

            string passwordQuestion = "";

            string comment = "";

            bool isApproved = false;

            bool isLockedOut = new Boolean();

            DateTime creationDate = new DateTime(); ;// DateTime.Parse(dv[0]["CreatedOn"].ToString());

            DateTime lastLoginDate = new DateTime();

            DateTime lastActivityDate = new DateTime();

            DateTime lastPasswordChangedDate = new DateTime();

            DateTime lastLockedOutDate = new DateTime();

            MembershipUser u = new MembershipUser("CustomMembershipProvider", username, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate,
            lastActivityDate, lastPasswordChangedDate, lastLockedOutDate);

            return u;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
