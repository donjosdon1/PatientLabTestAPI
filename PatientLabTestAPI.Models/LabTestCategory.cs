using PatientLabTestAPI.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PatientLabTestAPI.Models
{
    public class LabTestCategory : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryID { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [NotMapped]
        public Message Message { get; set; }
    }
}
