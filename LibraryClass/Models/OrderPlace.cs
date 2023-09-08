using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Models
{
    public class OrderPlace
    {
        public int OrderID { get; set; }

        public List<Item> Items { get; set; }

        public string Cust_Email { get; set; }
    }
}
