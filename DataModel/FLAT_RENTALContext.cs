using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlatRental.DataModel
{
    public partial class FLAT_RENTALContext : DbContext
    {
        public FLAT_RENTALContext()
        {
        }

        public FLAT_RENTALContext(DbContextOptions<FLAT_RENTALContext> options)
            : base(options)
        {
        }

        //Singletone
        private static FLAT_RENTALContext _context;

        public static FLAT_RENTALContext GetContext()
        {
            if (_context == null)
                _context = new FLAT_RENTALContext();

            return _context;
        }

        public virtual DbSet<Flat> Flats { get; set; } = null!;
        public virtual DbSet<Lease> Leases { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-SNMALJ9;Database=FLAT_RENTAL;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flat>(entity =>
            {
                entity.ToTable("FLATS");

                entity.Property(e => e.FlatId).HasColumnName("FLAT_ID");

                entity.Property(e => e.Area)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("AREA");

                entity.Property(e => e.Balcony)
                    .HasMaxLength(40)
                    .HasColumnName("BALCONY");

                entity.Property(e => e.Description)
                    .HasMaxLength(3500)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.District)
                    .HasMaxLength(40)
                    .HasColumnName("DISTRICT");

                entity.Property(e => e.Floor).HasColumnName("FLOOR");

                entity.Property(e => e.Image).HasColumnName("IMAGE");

                entity.Property(e => e.Metro)
                    .HasMaxLength(40)
                    .HasColumnName("METRO");

                entity.Property(e => e.NumberOfRooms).HasColumnName("NUMBER_OF_ROOMS");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE");

                entity.Property(e => e.RentalType)
                    .HasMaxLength(40)
                    .HasColumnName("RENTAL_TYPE");

                entity.Property(e => e.Toilet)
                    .HasMaxLength(40)
                    .HasColumnName("TOILET");

                entity.Property(e => e.Мicrodistrict)
                    .HasMaxLength(40)
                    .HasColumnName("МICRODISTRICT");
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.ToTable("LEASE");

                entity.Property(e => e.LeaseId).HasColumnName("LEASE_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.FlatId).HasColumnName("FLAT_ID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("START_DATE");

                entity.Property(e => e.TotalSum)
                    .HasColumnType("money")
                    .HasColumnName("TOTAL_SUM");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Flat)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.FlatId)
                    .HasConstraintName("FK__LEASE__FLAT_ID__2E1BDC42");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__LEASE__USER_ID__2F10007B");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("REVIEW");

                entity.Property(e => e.ReviewId).HasColumnName("REVIEW_ID");

                entity.Property(e => e.DateOfReview)
                    .HasColumnType("date")
                    .HasColumnName("DATE_OF_REVIEW");

                entity.Property(e => e.FlatId).HasColumnName("FLAT_ID");

                entity.Property(e => e.Text)
                    .HasMaxLength(3500)
                    .HasColumnName("TEXT");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Flat)
                    .WithMany()
                    .HasForeignKey(d => d.FlatId)
                    .HasConstraintName("FK__REVIEW__FLAT_ID__2B3F6F97");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__REVIEW__USER_ID__2A4B4B5E");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.Login)
                    .HasMaxLength(100)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(13)
                    .HasColumnName("PHONE_NUMBER");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__USERS__ROLE_ID__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
