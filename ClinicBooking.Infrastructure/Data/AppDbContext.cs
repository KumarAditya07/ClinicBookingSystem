using ClinicBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<DoctorSlot> DoctorSlots { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
           .HasOne(u => u.Role)
           .WithMany()
          .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
    .HasOne(d => d.Clinic)
    .WithMany(c => c.Doctors)
    .HasForeignKey(d => d.ClinicId);

            modelBuilder.Entity<DoctorSlot>()
    .HasOne(s => s.Doctor)
    .WithMany(d => d.Slots)
    .HasForeignKey(s => s.DoctorId);

            modelBuilder.Entity<Appointment>()
    .HasOne(a => a.User)
    .WithMany()
    .HasForeignKey(a => a.UserId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
    .HasOne(a => a.Doctor)
    .WithMany()
    .HasForeignKey(a => a.DoctorId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
    .HasOne(a => a.Slot)
    .WithOne()
    .HasForeignKey<Appointment>(a => a.SlotId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
       .HasOne(rt => rt.User)
       .WithMany(u => u.RefreshTokens)
       .HasForeignKey(rt => rt.UserId)
       .OnDelete(DeleteBehavior.Cascade);






            // seeding initial roles
            modelBuilder.Entity<Role>().HasData(
       new Role
       {
           Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
           RoleName = "Admin",
           CreatedAt = DateTime.UtcNow,
           UpdatedAt = DateTime.UtcNow,
           IsActive = true
       },
       new Role
       {
           Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
           RoleName = "Doctor",
           CreatedAt = DateTime.UtcNow,
           UpdatedAt = DateTime.UtcNow,
           IsActive = true
       },
       new Role
       {
           Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
           RoleName = "Patient",
           CreatedAt = DateTime.UtcNow,
           UpdatedAt = DateTime.UtcNow,
           IsActive = true
       });



        } 

    }


}
