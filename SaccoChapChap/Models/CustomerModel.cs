using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaccoChapChap.Models
{
    public class CustomerModel
    {
    }
    public class GridPassModel
    {
        public int ColumnID { get; set; }
        public string UniqueID { get; set; }
        public string PasswordType { get; set; }
        public string Remarks { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UserPasswordChangeModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "User Details required")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Remarks is required")]
        public string Remarks { get; set; }
    }

    public class ClientPINChangeModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Client Details required")]
        public string UniqueID { get; set; }

        [Required(ErrorMessage = "Remarks is required")]
        public string Remarks { get; set; }
    }

    public class RegisterClientModel
    {
        public int UniqueID { get; set; }
        [Required(ErrorMessage = "Client ID is required")]
        public string ClientID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string OtherNames { get; set; }

        [EmailAddress(ErrorMessage = "The Email address is not Valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }
        
        public string Remarks { get; set; }

    }

    public class UpdateClientRegisterModel
    {
        public int UniqueID { get; set; }
        [Required(ErrorMessage = "User Name/Login ID is required")]
        public string ClientID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string OtherNames { get; set; }

        [EmailAddress(ErrorMessage = "The Email address is not Valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }

        public string Remarks { get; set; }
    }
    public class ClientListModel
    {
        public Int32 UniqueID { get; set; }
        public int ColumnID { get; set; }
        public string CheckSumID { get; set; }
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string isActive { get; set; }
    }
}