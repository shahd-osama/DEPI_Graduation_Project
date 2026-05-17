using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TEFLY.DAL.Models;

namespace TEFLY.DAL.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "Parent", "HealthcareProvider" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var adminEmail = "admin@tefly.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "System Admin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        public static async Task SeedAsync(AppDbContext context)
        {
            if (!await context.VaccineSideEffects.AnyAsync())
            {
                context.VaccineSideEffects.AddRange(
                    new VaccineSideEffect { Name = "Fever", Description = "Mild to moderate fever", IsCommon = true },
                    new VaccineSideEffect { Name = "Redness at injection site", Description = "Local redness", IsCommon = true },
                    new VaccineSideEffect { Name = "Fatigue", Description = "Tiredness after vaccination", IsCommon = true },
                    new VaccineSideEffect { Name = "Allergic reaction", Description = "Rare severe reaction", IsCommon = false }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.HealthcareProviders.AnyAsync())
            {
                context.HealthcareProviders.AddRange(
                    new HealthcareProvider { Name = "City Children's Hospital", Type = "Hospital", Location = "Cairo", Phone = "01000000001" },
                    new HealthcareProvider { Name = "HealthFirst Clinic", Type = "Clinic", Location = "Alexandria", Phone = "01000000002" }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Vaccines.AnyAsync())
            {
                var bcg = new Vaccine { Name = "BCG", Description = "Tuberculosis vaccine", RecommendedAge = "Birth", DosageInfo = "0.05 ml intradermal" };
                var hepB = new Vaccine { Name = "Hepatitis B", Description = "Hepatitis B vaccine", RecommendedAge = "Birth", DosageInfo = "0.5 ml IM" };
                var mmr = new Vaccine { Name = "MMR", Description = "Measles, Mumps, Rubella", RecommendedAge = "12 months", DosageInfo = "0.5 ml SC" };
                context.Vaccines.AddRange(bcg, hepB, mmr);
                await context.SaveChangesAsync();

                context.VaccinationSchedules.AddRange(
                    new VaccinationSchedule { VaccineID = bcg.VaccineID, AgeStage = "Birth", DoseNumber = 1 },
                    new VaccinationSchedule { VaccineID = hepB.VaccineID, AgeStage = "Birth", DoseNumber = 1 },
                    new VaccinationSchedule { VaccineID = hepB.VaccineID, AgeStage = "2 months", DoseNumber = 2 },
                    new VaccinationSchedule { VaccineID = hepB.VaccineID, AgeStage = "6 months", DoseNumber = 3 },
                    new VaccinationSchedule { VaccineID = mmr.VaccineID, AgeStage = "12 months", DoseNumber = 1 }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.AwarenessItems.AnyAsync())
            {
                context.AwarenessItems.AddRange(
                    new Awareness { Title = "Why Vaccines Matter", Body = "Vaccines protect your child and the community.", Category = "Vaccine Safety", Tags = "safety,children", Status = "Published" },
                    new Awareness { Title = "Vaccination Schedule Guide", Body = "Follow the recommended schedule for full protection.", Category = "Schedules", Tags = "schedule,doses", Status = "Published" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
