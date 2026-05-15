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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccineSideEffect> VaccineSideEffects { get; set; }
        public DbSet<VaccineEffect> VaccineEffects { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }
        public DbSet<VaccinationSchedule> VaccinationSchedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<HealthcareProvider> HealthcareProviders { get; set; }
        public DbSet<AdverseReaction> AdverseReactions { get; set; }
        public DbSet<VaccineInventory> VaccineInventories { get; set; }

    }
}
