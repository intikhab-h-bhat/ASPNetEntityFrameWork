using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;

namespace ASPNetEntityFrameWork.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Please enter the student Name")]
        [StringLength (20)]
        public string Name { get; set; }    
        public string Address { get; set; }
        [EmailAddress(ErrorMessage ="Please enter valid email addres")]
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        [Range(10,20)]
        public int Age {  get; set; }

        public string Password {  get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword {  get; set; }

    }
}
