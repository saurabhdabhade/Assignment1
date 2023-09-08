using LibraryClass.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MVCApplication1.Models.ViewModel
{
    public class PlaceOrderViewModel
    {
        public int OrderID { get; set; }

        [MaxLength(20)]
        public string ItemName { get; set; }

        [MaxLength(30)]
        public string Cust_Email { get; set; }
        public int IQuantity { get; set; }
        public int IRate { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;
        public string ipAddress { get; set; } 

    }

    public class GetData
    {

        public List<string> ItemName { get; set; }
        public List<string> Cust_Email { get; set; }
        public List<int> IQuantity { get; set; }
        public List<int> IRate { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;
        public string ipAddress { get; set; }



    }
}
