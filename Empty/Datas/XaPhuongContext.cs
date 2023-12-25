using Empty.Models;
using Microsoft.EntityFrameworkCore;

namespace Empty.Datas
{
    public class XaPhuongContext : DbContext
    {
        public XaPhuongContext(DbContextOptions<XaPhuongContext> options) : base(options)
        {
        }
        public DbSet<XaPhuong> XaPhuongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<XaPhuong>()
                .HasKey(x => x.Id);
        }
    }
}
