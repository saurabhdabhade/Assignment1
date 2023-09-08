using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Models.DTO
{
    public class ItemDTO
    {
        [MaxLength(20)]
        public string ItemName { get; set; }
        public int IRate { get; set; }
        public int IQuantity { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;

    }
}
