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
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int IRate { get; set; }
        [Required]
        public int IQuantity { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;

        public bool deleteflag { get; set; }


    }
}
