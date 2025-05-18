using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hospital.Infrastructure.Persistance;
public class HospitalDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
{
    public HospitalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
        optionsBuilder.UseNpgsql("Host=postgres;Port=5432;Database=hospital;Username=root;Password=root");

        return new HospitalDbContext(optionsBuilder.Options);
    }
}