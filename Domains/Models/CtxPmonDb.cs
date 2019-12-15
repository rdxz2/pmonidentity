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

        public virtual DbSet<m_user> m_user { get; set; }
        public virtual DbSet<m_user_detail> m_user_detail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Name=PmonDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<m_user>(entity =>
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

            modelBuilder.Entity<m_user_detail>(entity =>
            {
                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.email).HasColumnType("varchar(255)");

                entity.Property(e => e.ext).HasColumnType("varchar(255)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.nik).HasColumnType("varchar(255)");

                entity.HasOne(d => d.idNavigation)
                    .WithOne(p => p.m_user_detail)
                    .HasForeignKey<m_user_detail>(d => d.id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_muserdetail_muser_id");
            });
        }
    }
}
