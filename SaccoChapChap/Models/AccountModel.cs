using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SaccoChapChap.Models
{

    public class GeneralModel
    {
        public Int32 jtStartIndex { get; set; }
        public Int32 jtPageSize { get; set; }
    }

    public class ListingModel
    {
        public Int32 jtStart { get; set; }
        public Int32 jtPageSize { get; set; }
    }

    public class ArrearsModel
    {
        public Int32 jtStartIndex { get; set; }
        public Int32 jtPageSize { get; set; }
    }

    public class breadCrumbModel
    {
        public IEnumerable<breadCrumbsModel> breadcrumb { get; set; }
    }

    public class breadCrumbsModel
    {
        public string controller { get; set; }
        public string action { get; set; }
    }
    public class PassHistoryModel
    {
        public Int32 jtStartIndex { get; set; }
        public Int32 jtPageSize { get; set; }
        public string PasswordType { get; set; }
        public Int32 UniqueID { get; set; }
    }

    public class userListModel
    {
        public Int32 UserID { get; set; }
        public int ColumnID { get; set; }
        public string CheckSumID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string isActive { get; set; }
    }

    public class UserAccessRights
    {
        public IEnumerable<ModuleAccess> moduleaccess { get; set; }
    }

    public class ModuleAccess
    {
        public int TaskID { get; set; }
        public int ParentTaskID { get; set; }
        public string TaskName { get; set; }
        public int TaskStatus { get; set; }
    }

    public class RegisterUserModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "User Name/Login ID is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password   is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string OtherNames { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "The Email address is not Valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }
    }

    public class UpdateRegisterModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "User Name/Login ID is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string OtherNames { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "The Email address is not Valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }

        public string Remarks { get; set; }
    }

    public class PasswordUserModel
    {
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class ModifyPasswordModel
    {
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class Roles
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class AccessRoles
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class RegisterRoleModel
    {
        [Display(Name = "User name")]
        public string RoleID { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class EditRoleProfile
    {
        public int TaskID { get; set; }
        public int TaskStatus { get; set; }
    }
    public class ProfileObj
    {
        public string TaskID { get; set; }
        public string TaskStatus { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    //public class UserList
    //{
    //    public int ColumnID { get; set; }
    //    public int UserID { get; set; }
    //    public string UserName { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Email { get; set; }
    //    public string Mobile { get; set; }
    //}
}
