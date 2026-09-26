using Microsoft.EntityFrameworkCore;
using Sejily.API.Models.Entities;

namespace Sejily.API.Data;

public class SejilyDbContext : DbContext
{
    public SejilyDbContext(DbContextOptions<SejilyDbContext> options)
        : base(options)
    {
    }

    // =========================
    // DbSets
    // =========================

    public DbSet<User> Users { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Guardian> Guardians { get; set; }

    public DbSet<ChronicDisease> ChronicDiseases { get; set; }

    public DbSet<Allergy> Allergies { get; set; }

    public DbSet<Medication> Medications { get; set; }

    public DbSet<Workplace> Workplaces { get; set; }

    public DbSet<DoctorWorkplace> DoctorWorkplaces { get; set; }

    public DbSet<GuardianPatient> GuardianPatients { get; set; }

    public DbSet<PatientChronicDisease> PatientChronicDiseases { get; set; }

    public DbSet<PatientAllergy> PatientAllergies { get; set; }

    public DbSet<PatientFrequentMedication> PatientFrequentMedications { get; set; }


    // =========================
    // Model Configuration
    // =========================

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // =========================
        // User
        // =========================

        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.NationalID)
            .HasMaxLength(14)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.NationalID)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.FullName)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Address)
            .HasMaxLength(500)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Phone)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();


        // =========================
        // Patient
        // =========================

        modelBuilder.Entity<Patient>()
            .HasKey(p => p.UserId);

        modelBuilder.Entity<Patient>()
            .Property(p => p.HealthCardNumber)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.HealthCardNumber)
            .IsUnique();

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        // =========================
        // Doctor
        // =========================

        modelBuilder.Entity<Doctor>()
            .HasKey(d => d.UserId);

        modelBuilder.Entity<Doctor>()
            .Property(d => d.SyndicateCardNumber)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Doctor>()
            .Property(d => d.MedicalLicenseNumber)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Doctor>()
            .Property(d => d.Specialization)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.SyndicateCardNumber)
            .IsUnique();

        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.MedicalLicenseNumber)
            .IsUnique();

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.User)
            .WithOne(u => u.Doctor)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        // =========================
        // Guardian
        // =========================

        modelBuilder.Entity<Guardian>()
            .HasKey(g => g.UserId);

        modelBuilder.Entity<Guardian>()
            .HasOne(g => g.User)
            .WithOne(u => u.Guardian)
            .HasForeignKey<Guardian>(g => g.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        // =========================
        // Chronic Disease
        // =========================

        modelBuilder.Entity<ChronicDisease>()
            .HasKey(d => d.DiseaseId);

        modelBuilder.Entity<ChronicDisease>()
            .Property(d => d.Name)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<ChronicDisease>()
            .Property(d => d.Description)
            .HasMaxLength(1000);


        // =========================
        // Allergy
        // =========================

        modelBuilder.Entity<Allergy>()
            .HasKey(a => a.AllergyId);

        modelBuilder.Entity<Allergy>()
            .Property(a => a.Name)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Allergy>()
            .Property(a => a.Description)
            .HasMaxLength(500);


        // =========================
        // Medication
        // =========================

        modelBuilder.Entity<Medication>()
            .HasKey(m => m.MedicationId);

        modelBuilder.Entity<Medication>()
            .Property(m => m.Name)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Medication>()
            .Property(m => m.Description)
            .HasMaxLength(1000);


        // =========================
        // Workplace
        // =========================

        modelBuilder.Entity<Workplace>()
            .HasKey(w => w.WorkplaceId);

        modelBuilder.Entity<Workplace>()
            .Property(w => w.Name)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Workplace>()
            .Property(w => w.Type)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<Workplace>()
            .Property(w => w.Address)
            .HasMaxLength(500)
            .IsRequired();

        modelBuilder.Entity<Workplace>()
            .Property(w => w.LicenseNumber)
            .HasMaxLength(100)
            .IsRequired();


        // =========================
        // DoctorWorkplace
        // =========================

        // Composite Primary Key
        modelBuilder.Entity<DoctorWorkplace>()
            .HasKey(dw => new
            {
                dw.DoctorId,
                dw.WorkplaceId
            });

        // Doctor -> DoctorWorkplace
        modelBuilder.Entity<DoctorWorkplace>()
            .HasOne(dw => dw.Doctor)
            .WithMany(d => d.Workplaces)
            .HasForeignKey(dw => dw.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Workplace -> DoctorWorkplace
        modelBuilder.Entity<DoctorWorkplace>()
            .HasOne(dw => dw.Workplace)
            .WithMany(w => w.Doctors)
            .HasForeignKey(dw => dw.WorkplaceId)
            .OnDelete(DeleteBehavior.Cascade);


        // =========================
        // GuardianPatient
        // =========================

        // Composite Primary Key
        modelBuilder.Entity<GuardianPatient>()
            .HasKey(gp => new
            {
                gp.GuardianId,
                gp.PatientId
            });

        // Guardian -> GuardianPatient
        modelBuilder.Entity<GuardianPatient>()
            .HasOne(gp => gp.Guardian)
            .WithMany(g => g.Patients)
            .HasForeignKey(gp => gp.GuardianId)
            .OnDelete(DeleteBehavior.Cascade);

        // Patient -> GuardianPatient
        modelBuilder.Entity<GuardianPatient>()
            .HasOne(gp => gp.Patient)
            .WithMany(p => p.Guardians)
            .HasForeignKey(gp => gp.PatientId)
            .OnDelete(DeleteBehavior.Cascade);


        // =========================
        // PatientChronicDisease
        // =========================

        // Composite Primary Key
        modelBuilder.Entity<PatientChronicDisease>()
            .HasKey(pcd => new
            {
                pcd.PatientId,
                pcd.DiseaseId
            });

        // Patient -> PatientChronicDisease
        modelBuilder.Entity<PatientChronicDisease>()
            .HasOne(pcd => pcd.Patient)
            .WithMany(p => p.ChronicDiseases)
            .HasForeignKey(pcd => pcd.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // ChronicDisease -> PatientChronicDisease
        modelBuilder.Entity<PatientChronicDisease>()
            .HasOne(pcd => pcd.Disease)
            .WithMany(d => d.Patients)
            .HasForeignKey(pcd => pcd.DiseaseId)
            .OnDelete(DeleteBehavior.Cascade);


        // =========================
        // PatientAllergy
        // =========================

        // Composite Primary Key
        modelBuilder.Entity<PatientAllergy>()
            .HasKey(pa => new
            {
                pa.PatientId,
                pa.AllergyId
            });

        // Patient -> PatientAllergy
        modelBuilder.Entity<PatientAllergy>()
            .HasOne(pa => pa.Patient)
            .WithMany(p => p.Allergies)
            .HasForeignKey(pa => pa.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Allergy -> PatientAllergy
        modelBuilder.Entity<PatientAllergy>()
            .HasOne(pa => pa.Allergy)
            .WithMany(a => a.Patients)
            .HasForeignKey(pa => pa.AllergyId)
            .OnDelete(DeleteBehavior.Cascade);


        // =========================
        // PatientFrequentMedication
        // =========================

        // Composite Primary Key
        modelBuilder.Entity<PatientFrequentMedication>()
            .HasKey(pfm => new
            {
                pfm.PatientId,
                pfm.MedicationId
            });

        // Patient -> PatientFrequentMedication
        modelBuilder.Entity<PatientFrequentMedication>()
            .HasOne(pfm => pfm.Patient)
            .WithMany(p => p.FrequentMedications)
            .HasForeignKey(pfm => pfm.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Medication -> PatientFrequentMedication
        modelBuilder.Entity<PatientFrequentMedication>()
            .HasOne(pfm => pfm.Medication)
            .WithMany(m => m.Patients)
            .HasForeignKey(pfm => pfm.MedicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}