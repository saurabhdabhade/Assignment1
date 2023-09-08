using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraryClass.Models.DTO
{
    public class PlaceOrderDTO
    {
        public int OrderID { get; set; }
        //public List<Item> ItemName { get; set; }
        public string ItemName { get; set; }

        public string Cust_Email { get; set; }
        public int IQuantity { get; set; }
        public int IRate { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;

        public string ipAddress { get; set; }
    }
}
