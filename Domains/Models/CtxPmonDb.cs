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
                entity.HasIndex(e => e.cb)
                    .HasName("fk_muser_muser_cb");

                entity.HasIndex(e => e.ub)
                    .HasName("fk_muser_muser_ub");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.active).HasColumnType("bit(1)");

                entity.Property(e => e.cb).HasColumnType("int(11)");

                entity.Property(e => e.cd).HasColumnType("datetime");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ub).HasColumnType("int(11)");

                entity.Property(e => e.ud).HasColumnType("datetime");

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.cbNavigation)
                    .WithMany(p => p.InversecbNavigation)
                    .HasForeignKey(d => d.cb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_muser_muser_cb");

                entity.HasOne(d => d.ubNavigation)
                    .WithMany(p => p.InverseubNavigation)
                    .HasForeignKey(d => d.ub)
                    .HasConstraintName("fk_muser_muser_ub");
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
