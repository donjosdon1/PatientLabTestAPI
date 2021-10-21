using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientLabTestAPI.Models
{
    public abstract class ModelBase
    {
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public string LastUpdatedBy { get; set; }
    }
}
