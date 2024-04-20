
using System.ComponentModel.DataAnnotations; 


namespace Application.DTOs.UserSide.Account
{
    public class SignUpDto
    {
        [MaxLength(30)]
        public string UserName { get; set; }

        public string Email { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password And Repeat Paswword Are Not Same!!")]
        [MinLength(8)]
        public string RepeatPassword { get; set; }
        public string PhoneNumber { get; set; }

    }
}
