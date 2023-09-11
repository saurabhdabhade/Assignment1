using LibraryClass.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCApplication1.Models.ViewModel
{
    public class ItemViewModel
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

    public class GetItems
    {
        [MaxLength(20)]
        [Required]
        public List<string> ItemName { get; set; }
        [Required]
        public List<int> IRate { get; set; }
        [Required]
        public List<int> IQuantity { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;

        public bool deleteflag { get; set; }

    }
}
