using Microsoft.EntityFrameworkCore;

namespace Tecnica.Data
{
    public class TecnicaContext : DbContext
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TecnicaContext>(options =>
                options.UseInMemoryDatabase("TecnicaDatabase"));
        }
        public TecnicaContext(DbContextOptions<TecnicaContext> options)
            : base(options)
        {
        }

        public DbSet<Tecnica.Models.RoleModel> RoleModel { get; set; } = default!;

        public DbSet<Tecnica.Models.UsuarioModel> UsuarioModel { get; set; } = default!;

        public DbSet<Tecnica.Models.PermisoModel> PermisoModel { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("TecnicaDatabase");
            }
        }
    }
}
