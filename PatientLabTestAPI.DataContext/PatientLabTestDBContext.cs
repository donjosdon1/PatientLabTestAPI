using Microsoft.EntityFrameworkCore;
using PatientLabTestAPI.Models;

namespace PatientLabTestAPI.Repository
{
    public class PatientLabTestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=PatientLabTest.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Patient>()
                .HasOne(a => a.PatientPrimaryContact).WithOne(b => b.Patient)                
                .HasForeignKey<PatientContact>(e => e.PatientID);

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientContact> PatientContacts { get; set; }
        public DbSet<LabTestCategory> LabTestCategories { get; set; }
        public DbSet<LabTestSubCategory> LabTestSubCategories { get; set; }
        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<PatientLabResults> PatientLabResults { get; set; }
    }
}
