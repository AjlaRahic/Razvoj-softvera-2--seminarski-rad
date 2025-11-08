using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eFood.Services.Database
{
    public class EFoodContextFactory : IDesignTimeDbContextFactory<EFoodContext>
    {
        public EFoodContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFoodContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-HOR3B84;Initial Catalog=eFood;User ID=sa;Password=Lozinka123;TrustServerCertificate=True");

            return new EFoodContext(optionsBuilder.Options);
        }
    }
}
