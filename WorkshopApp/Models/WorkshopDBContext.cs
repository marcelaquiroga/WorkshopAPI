using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WorkshopApp.Models
{
    public partial class WorkshopDBContext : DbContext
    {
        public WorkshopDBContext()
        {
        }

        public WorkshopDBContext(DbContextOptions<WorkshopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<SaleDetail> SaleDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sale)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Sale");
            });

            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.HasKey(e => new { e.SaleDetailId, e.ProductId })
                    .HasName("PK_OrderDetail");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SaleDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_OrderDetail");

                entity.HasOne(d => d.SaleDetailNavigation)
                    .WithMany(p => p.SaleDetail)
                    .HasForeignKey(d => d.SaleDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sale_SaleDetail");
            });
        }
    }
}
