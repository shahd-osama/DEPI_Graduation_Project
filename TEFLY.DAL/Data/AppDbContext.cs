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
    }
}
