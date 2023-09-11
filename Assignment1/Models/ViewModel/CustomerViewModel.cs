using LibraryClass.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCApplication1.Models.ViewModel
{
    public class CustomerViewModel 
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        [StringLength(30)]
        public string Cust_Email { get; set; }

        [MaxLength(30)]
        [Required]
        public string Cust_FirstName { get; set; }

        [MaxLength(30)]
        [Required]
        public string Cust_LastName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        [Required]
        public long Cust_Phone { get; set; }

        public DateTime EventDateTime { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

    }


    public class GetCustomerData
    {
        [Required]
        public List<string> Cust_FirstName { get; set; }
        [Required]
        public List<string> Cust_LastName { get; set; }

        [Required]
        public List<long> Cust_Phone { get; set; }

        [Required]
        public List<string> Cust_Email { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }


    }
}
