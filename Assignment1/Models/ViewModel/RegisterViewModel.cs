using System.ComponentModel.DataAnnotations;

namespace MVCApplication1.Models.ViewModel
{
    public class RegisterViewModel
    {
        public int RegisterID { get; set; }

        [MaxLength(30)]
        [Required]
        public string First_Name { get; set; }

        [MaxLength(30)]
        [Required]
        public string Last_Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        [EmailAddress]
        [StringLength(30)]
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\W)(?!.*\s).{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least 1 capital letter, and 1 special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Confirm_Password { get; set; }

        public string LastPassword1 { get; set; }
        public string LastPassword2 { get; set; }

        public DateTime EventDateTime { get; set; } = DateTime.Now;

    }
}
