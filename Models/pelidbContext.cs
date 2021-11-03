using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PeliRestApi.Models
{
    public partial class pelidbContext : DbContext
    {
        public pelidbContext()
        {
        }

        public pelidbContext(DbContextOptions<pelidbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genret> Genrets { get; set; }
        public virtual DbSet<Pelit> Pelits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4BCAT9M\\SQLEXPRESS;Database=pelidb;Trusted_Connection=True;");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Genret>(entity =>
            {
                entity.HasKey(e => e.GenreId);

                entity.ToTable("Genret");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Kuvaus).HasMaxLength(100);

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Pelit>(entity =>
            {
                entity.HasKey(e => e.PeliId);

                entity.ToTable("Pelit");

                entity.Property(e => e.PeliId).HasColumnName("PeliID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Pelits)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pelit_Genret");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
