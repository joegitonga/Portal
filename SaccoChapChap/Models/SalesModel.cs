using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SaccoChapChap.Models
{
    public class SalesModel
    {
        public string AccountTrxID { get; set; }
        public string TrxBranchID { get; set; }
        public string SerialID { get; set; }
        public string AccountTypeID { get; set; }
        public string AccountID { get; set; }
        public string ProductID { get; set; }
        public string TrxDate { get; set; }
        public string TrxTypeID { get; set; }
        public string TrxCurrencyID { get; set; }
        public string ValueDate { get; set; }
        public string TrxAmount { get; set; }
        public string TrxDescriptionID { get; set; }
        public string TrxDescription { get; set; }
     }
}