using Microsoft.EntityFrameworkCore;

namespace WebApplication15.Models
{
    public class db_urunTakipContext: DbContext
    {
        public db_urunTakipContext(DbContextOptions<db_urunTakipContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TblKategoriler> TblKategorilers { get; set; } = null;
        public virtual DbSet<TblUrun> TblUruns { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=OMEN\\SQLEXPRESS;Database=db_uruntakip;trusted_Connection=True;encrypt=false;");
            }
        }
    }
}
