using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Models.DTO
{
    public class CustomerDTO
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
}
