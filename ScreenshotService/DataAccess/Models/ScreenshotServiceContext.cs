using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public partial class ScreenshotServiceContext : DbContext
    {
        private readonly string _connectionString;

        public ScreenshotServiceContext(string connectionStringArg)
        {
            _connectionString = connectionStringArg;
        }

        public ScreenshotServiceContext(DbContextOptions<ScreenshotServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Screenshot> Screenshot { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Screenshot>(entity =>
            {
                entity.Property(e => e.Cerated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
