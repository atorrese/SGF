using Microsoft.EntityFrameworkCore;
using SGF.Models;

namespace SGF.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        public DbSet<Contacto> Contacto { get; set; }
    }
}
