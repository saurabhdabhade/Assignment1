using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Models
{
    public class PlaceOrder
    {
        [Key]
        public int OrderID {get; set;}

        [ForeignKey("ItemName")]
        [MaxLength(20)]
        public string ItemName { get; set;}

        [ForeignKey("Cust_Email")]
        [StringLength(30)]
        public string Cust_Email { get; set; }
        public int IQuantity { get; set; }

        [ForeignKey("IRate")]
        public int IRate { get; set; }

        public DateTime EventDateTime { get; set; } = DateTime.Now;
        public string ipAddress { get; set; }

        public bool IsDeleted { get; set; }

    }
}
