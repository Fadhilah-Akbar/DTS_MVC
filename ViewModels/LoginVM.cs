using System.ComponentModel.DataAnnotations;

namespace DTS_WebApplication.ViewModels
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }

        // Password
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
