using Microsoft.EntityFrameworkCore;

namespace prs_server.Models
{
    public class PrsContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }

        public PrsContext()
        { }

        public PrsContext(DbContextOptions<PrsContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e => e.HasIndex(x => x.Username).IsUnique(true));
            builder.Entity<Vendor>(e => e.HasIndex(x => x.Code).IsUnique(true));
            builder.Entity<Product>(e => e.HasIndex(x => x.PartNbr).IsUnique(true));
        }
    }
}