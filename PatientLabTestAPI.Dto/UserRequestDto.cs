using System.ComponentModel.DataAnnotations;

namespace PatientLabTestAPI.Dto
{
    public class UserRequestDto
    {        
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]        
        public string Role { get; set; }        
    }
}
