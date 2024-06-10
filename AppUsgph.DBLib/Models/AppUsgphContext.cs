using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppUsgph.DBLib.Models
{
    public partial class AppUsgphContext : DbContext
    {
        #region Properties

        /// <summary>
        /// Obtient ou défini la liste des utilisateurs
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des événements
        /// </summary>
        public DbSet<Evenement> Evenements { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des type d'événement
        /// </summary>
        public DbSet<TypeEvenement> TypeEvenements { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Surcharge de la méthode OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
    ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString,
    new MySqlServerVersion("11.2.2-MariaDB-1:11.2.2+maria~ubu2004"),
    options => options.EnableRetryOnFailure()
        );


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.ToTable("evenements");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("titre");
                entity.Property(e => e.Location)
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .HasColumnName("lieu");
                entity.Property(e => e.Date)
                     .HasColumnType("date")
                     .HasColumnName("date");
                entity.Property(d => d.TypeEvenementId)
                .HasColumnName("type_evenement_id");
                entity.HasOne(d => d.Type).WithMany(p => p.Evenement)
                .HasForeignKey(d => d.TypeEvenementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("type_evenement_id");
                entity.Property(e => e.CreatedAt)
                      .HasColumnType("timestamp")
                      .HasColumnName("created_at");
                entity.Property(d => d.UserIdCreate)
                .HasColumnName("user_id_creation");
                entity.HasOne(d => d.UserCreation).WithMany(p => p.Evenement)
                .HasForeignKey(d => d.UserIdCreate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("type_evenements_user_id_creation_foreign");
            });

            modelBuilder.Entity<TypeEvenement>(entity =>
            {
                entity.ToTable("type_evenements");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("libelle");
                entity.Property(e => e.CreatedAt)
                      .HasColumnType("timestamp")
                      .HasColumnName("created_at");
                entity.Property(d => d.UserIdCreate)
                .HasColumnName("user_id_creation");
                entity.HasOne(d => d.UserCreation).WithMany(p => p.TypeEvenement)
                .HasForeignKey(d => d.UserIdCreate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("type_evenements_user_id_creation_foreign");
                

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_User");
                entity.ToTable("users");
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("last_name");
                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.HashedPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt)
                      .HasColumnType("timestamp")
                      .HasColumnName("updated_at");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion
    }
}
