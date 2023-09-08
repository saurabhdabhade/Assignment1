using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Models.DTO
{
    public class LoginResponse
    {
        public string User { get; set; }
        public string Token { get; set; }
    }
}
