﻿using PatientLabTestAPI.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PatientLabTestAPI.Models
{
    public class LabTestSubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SubCategoryID { get; set; }
        [ForeignKey(nameof(LabTestCategory))]
        public long CategoryID { get; set; }
        [Required]
        [MaxLength(100)]
        public string SubCategoryName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual LabTestCategory LabTestCategory { get; set; }
        [NotMapped]
        public Message Message { get; set; }
    }
}
