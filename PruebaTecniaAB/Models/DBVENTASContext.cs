using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaTecniaAB.Models
{
    public partial class DBVENTASContext : DbContext
    {
        public DBVENTASContext()
        {
        }

        public DBVENTASContext(DbContextOptions<DBVENTASContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<SalesProduct> SalesProducts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Product__2E8946D4C3949005");

                entity.ToTable("Product");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.IdSale);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Mail).HasMaxLength(100);

                entity.Property(e => e.NameClient).HasMaxLength(50);

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("money");
            });

            modelBuilder.Entity<SalesProduct>(entity =>
            {
                entity.HasKey(e => e.IdSalesProduct);

                entity.HasOne(d => d.oProduct)
                    .WithMany(p => p.SalesProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesProducts_Product");

                entity.HasOne(d => d.IdSaleNavigation)
                    .WithMany(p => p.SalesProducts)
                    .HasForeignKey(d => d.IdSale)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesProducts_Sales");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Mail).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
