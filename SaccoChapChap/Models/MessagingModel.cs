using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaccoChapChap.Models
{
    public class SendSMSModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Bank ID is required")]
        public string BankID { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Sms Message is required")]
        public string Description { get; set; }
    }

    public class AdhocSMSModel
    {
        public string BankID { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Sms Message is required")]
        public string Description { get; set; }
    }
    public class LoanListModel
    {
        public Int32 UniqueID { get; set; }
        public int ColumnID { get; set; }
        public string MemberNo { get; set; }
        public string Name { get; set; }
        public string LoanNo { get; set; }
        public string DateDisb { get; set; }
        public string AmountDisb { get; set; }
        public string IntAmount { get; set; }
        public string LoanType { get; set; }
        public string ChequeID { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }

    public class ExcelUploadModel
    {
        [Key]
        public string UniqueID { get; set; }
        public string AccountID { get; set; }
        public string UploadedExcelNo { get; set; }
        public string ExcelFileUniqueID { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string filePath { get; set; }
    }

    public class SmsListingModel
    {
        public int SmsID { get; set; }
        public string UniqueID { get; set; }
        public string BankID { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }
        public string Status { get; set; }
        public string MessageId { get; set; }
        public string ErrorMessage { get; set; }
        public string ReferenceID { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class ArrearsListModel
    {
        public Int32 UniqueID { get; set; }
       //public int ColumnID { get; set; }
        public string MemberNo { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string LoanNo { get; set; }
        public string DateDisb { get; set; }
        public string AmountDisb { get; set; }
        public string PrincipalDue { get; set; }
        public string InterestDue { get; set; }
        public string TotalDue { get; set; }
        public string LoanType { get; set; }
        public string ChequeID { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }

    public class PdcSmsDetailsModel
    {
        public Int32 UniqueID { get; set; }
        public string MemberNo { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string ChequeDate { get; set; }
        public string PDCChequeStatusID { get; set; }
        public string ChequeAmount { get; set; }
        public string Status { get; set; }
        public string DateSent { get; set; }
        public string SentBy { get; set; }
        public string ChequeID { get; set; }
        public string Remarks { get; set; }
    }
}