using System.ComponentModel.DataAnnotations;

namespace ASPNetEntityFrameWork.Models
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public LoginDTO()
        {
            UserName = "Intikhab";
            Password = "abc@123";
        }

    }
}
