using Hospital.Domain.Entities.Authentication;
using Hospital.Domain.Entities.Devices;
using Hospital.Domain.Entities.Locations;
using Hospital.Domain.Entities.MedicalHistory;
using Hospital.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hospital.Infrastructure.Persistance;

public class HospitalDbContext(DbContextOptions<HospitalDbContext> options) : DbContext(options)
{
    public DbSet<Bed> Beds { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<GeneralPractitionerProfile> GeneralPractitionerProfiles { get; set; }
    public DbSet<GeneralPractitionerCheckUp> GeneralPractitionerCheckUps { get; set; }
    public DbSet<SpecialistProfile> SpecialistProfiles { get; set; }
    public DbSet<Domain.Entities.Locations.Hospital> Hospitals { get; set; }
    public DbSet<PatientMedicalHistory> MedicalHistories { get; set; }
    public DbSet<MedicalDevice> MedicalDevices { get; set; }
    public DbSet<NurseProfile> NurseProfiles { get; set; }
    public DbSet<PatientProfile> PatientProfiles { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionLine> PrescriptionLines { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<StorageUnit> StorageUnits { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplySnakeCaseNaming(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);
    }

    private static void ApplySnakeCaseNaming(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Rename table
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
                entity.SetTableName(ToSnakeCase(tableName));

            // Rename columns
            foreach (var property in entity.GetProperties())
            {
                var columnName = property.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName()!, null));
                if (!string.IsNullOrEmpty(columnName))
                    property.SetColumnName(ToSnakeCase(columnName));
            }
        }
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsUpper(c))
            {
                if (i > 0)
                    sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}