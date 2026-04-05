using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyAccount.Models
{
    public class AddFormRawDataDAO
    {
        [DisplayName("AccountName")]
        public string AccountName { get; set; }

        [DisplayName("AccountType")]
        public string AccountType { get; set; }

        [DisplayName("Date")]
        public string Date { get; set; }

        [DisplayName("Detail")]
        public string Detail { get; set; }

        [DisplayName("PaymentMethod")]
        public string PaymentMethod { get; set; }

        [DisplayName("Amount")]
        public string Amount { get; set; }

        [DisplayName("Picture1Path")]
        public string Picture1Path { get; set; }

        [DisplayName("Picture2Path")]
        public string Picture2Path { get; set; }

        [DisplayName("CompressedPicture1Path")]
        public string CompressedPicture1Path { get; set; }

        [DisplayName("CompressedPicture2Path")]
        public string CompressedPicture2Path { get; set; }
    }
}
