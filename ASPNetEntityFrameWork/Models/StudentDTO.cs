using System.ComponentModel.DataAnnotations;

namespace ASPNetEntityFrameWork.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }    
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DOB { get; set; }

    }
}
