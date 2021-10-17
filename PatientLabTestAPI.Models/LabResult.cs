using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PatientLabTestAPI.Models
{
    public class LabResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ResultID { get; set; }
        [Required]
        [ForeignKey("LabTestSubCategory")]
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
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual LabTestSubCategory LabTestSubCategory { get; set; }
    }
}
