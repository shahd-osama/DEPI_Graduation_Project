using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TEFLY.DAL.Models;

namespace TEFLY.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ── All 14 ERD entities ────────────────────────────────
        public DbSet<Child> Children { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccineSideEffect> VaccineSideEffects { get; set; }
        public DbSet<VaccineEffect> VaccineEffects { get; set; }
        public DbSet<VaccinationSchedule> VaccinationSchedules { get; set; }
        public DbSet<HealthcareProvider> HealthcareProviders { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AdverseReaction> AdverseReactions { get; set; }
        public DbSet<VaccineInventory> VaccineInventories { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Awareness> AwarenessItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ── VaccineEffect: composite primary key ───────────
            builder.Entity<VaccineEffect>()
                .HasKey(ve => new { ve.VaccineID, ve.EffectID });

            // ── Child → ApplicationUser (restrict to avoid cycles)
            builder.Entity<Child>()
                .HasOne(c => c.User)
                .WithMany(u => u.Children)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Notification → ApplicationUser
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Complaint → ApplicationUser
            builder.Entity<Complaint>()
                .HasOne(c => c.User)
                .WithMany(u => u.Complaints)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Appointment: restrict cascade (multiple FKs to Child/Vaccine/Provider)
            builder.Entity<Appointment>()
                .HasOne(a => a.Child)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ChildID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Vaccine)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VaccineID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Provider)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.ProviderID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── VaccinationRecord: restrict cascade
            builder.Entity<VaccinationRecord>()
                .HasOne(r => r.Child)
                .WithMany(c => c.VaccinationRecords)
                .HasForeignKey(r => r.ChildID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VaccinationRecord>()
                .HasOne(r => r.Vaccine)
                .WithMany(v => v.VaccinationRecords)
                .HasForeignKey(r => r.VaccineID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VaccinationRecord>()
                .HasOne(r => r.Provider)
                .WithMany(p => p.VaccinationRecords)
                .HasForeignKey(r => r.ProviderID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── AdverseReaction: restrict cascade
            builder.Entity<AdverseReaction>()
                .HasOne(ar => ar.Child)
                .WithMany(c => c.AdverseReactions)
                .HasForeignKey(ar => ar.ChildID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AdverseReaction>()
                .HasOne(ar => ar.Vaccine)
                .WithMany(v => v.AdverseReactions)
                .HasForeignKey(ar => ar.VaccineID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── VaccineInventory: restrict cascade
            builder.Entity<VaccineInventory>()
                .HasOne(vi => vi.Vaccine)
                .WithMany(v => v.VaccineInventories)
                .HasForeignKey(vi => vi.VaccineID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VaccineInventory>()
                .HasOne(vi => vi.Provider)
                .WithMany(p => p.VaccineInventories)
                .HasForeignKey(vi => vi.ProviderID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── VaccineEffect: no cascade on either side
            builder.Entity<VaccineEffect>()
                .HasOne(ve => ve.Vaccine)
                .WithMany(v => v.VaccineEffects)
                .HasForeignKey(ve => ve.VaccineID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<VaccineEffect>()
                .HasOne(ve => ve.SideEffect)
                .WithMany(se => se.VaccineEffects)
                .HasForeignKey(ve => ve.EffectID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── VaccinationSchedule: cascade from Vaccine
            builder.Entity<VaccinationSchedule>()
                .HasOne(vs => vs.Vaccine)
                .WithMany(v => v.VaccinationSchedules)
                .HasForeignKey(vs => vs.VaccineID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
