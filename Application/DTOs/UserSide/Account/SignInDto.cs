using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserSide.Account
{
    public class SignInDto
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string?  UserAvatar { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? SuperAdmin { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
