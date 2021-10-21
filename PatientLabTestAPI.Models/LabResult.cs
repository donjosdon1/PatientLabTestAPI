using PatientLabTestAPI.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PatientLabTestAPI.Models
{
    public class LabResult : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ResultID { get; set; }
        [Required]
        [ForeignKey(nameof(LabTestSubCategory))]
        public long SubCategoryID { get; set; }        
        [Required]
        [MaxLength(100)]
        public string ResultType { get; set; }  
        [Required]
        public int LowRange { get; set; }
        [Required]
        public int HighRange { get; set; }
        [Required]
        public string ResultUnit { get; set; }
        [Required]
        public string ResultDescription { get; set; }
        public virtual LabTestSubCategory LabTestSubCategory { get; set; }
        [NotMapped]
        public Message Message { get; set; }
    }
}
