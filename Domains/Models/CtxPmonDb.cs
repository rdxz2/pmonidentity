using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace pmonidentity.Domains.Models
{
    public partial class CtxPmonDb : DbContext
    {
        public CtxPmonDb()
        {
        }

        public CtxPmonDb(DbContextOptions<CtxPmonDb> options)
            : base(options)
        {
        }

        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<user_detail> user_detail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Name=PmonDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>(entity =>
            {
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.cd).HasColumnType("datetime");

                entity.Property(e => e.is_active).HasColumnType("bit(1)");

                entity.Property(e => e.md).HasColumnType("datetime");

                entity.Property(e => e.md_password).HasColumnType("datetime");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<user_detail>(entity =>
            {
                entity.HasKey(e => e.id_user)
                    .HasName("PRIMARY");

                entity.Property(e => e.id_user).HasColumnType("int(11)");

                entity.Property(e => e.email).HasColumnType("varchar(255)");

                entity.Property(e => e.ext).HasColumnType("varchar(255)");

                entity.Property(e => e.image).HasColumnType("varchar(255)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.name_shorthand)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.nik).HasColumnType("varchar(255)");

                entity.HasOne(d => d.id_userNavigation)
                    .WithOne(p => p.user_detail)
                    .HasForeignKey<user_detail>(d => d.id_user)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userdetail_user_iduser");
            });
        }
    }
}
