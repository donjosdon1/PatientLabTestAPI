using PatientLabTestAPI.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientLabTestAPI.Dto
{
    public class UserRequestDto
    {        
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]        
        public string Role { get; set; }        
    }
}
