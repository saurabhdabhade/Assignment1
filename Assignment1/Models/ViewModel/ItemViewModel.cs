using LibraryClass.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCApplication1.Models.ViewModel
{
    public class ItemViewModel
    {
        [MaxLength(20)]
        public string ItemName { get; set; }
        public int IRate { get; set; }
        public int IQuantity { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;

    }
}
