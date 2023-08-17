using Microsoft.EntityFrameworkCore;
using GiveFreely.Common;

namespace GiveFreely.DataAccess
{
    public class GFContext : DbContext
    {
        public GFContext(DbContextOptions<GFContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schema.Customer>().ToTable(DBTables.DBCustomers).HasOne(e => e.Affiliate).WithMany(c => c.Customers).HasForeignKey(p => p.IdAffiliate);
            modelBuilder.Entity<Schema.Commision>().ToTable(DBTables.DBCommisions);
            modelBuilder.Entity<Schema.Affiliate>().ToTable(DBTables.DBAffiliates).HasMany(e => e.Customers).WithOne(c => c.Affiliate).HasForeignKey(p => p.IdAffiliate).OnDelete(DeleteBehavior.Restrict);
        }


        public virtual DbSet<Schema.Customer> Customers { get; set; }
        public virtual DbSet<Schema.Commision> Commisions { get; set; }
        public virtual DbSet<Schema.Affiliate> Affiliates { get; set; }
    }
}
