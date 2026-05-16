using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TEFLY.BLL.Mapping;
using TEFLY.BLL.Services;
using TEFLY.BLL.Services.Interfaces;
using TEFLY.DAL.Data;
using TEFLY.DAL.Models;
using TEFLY.DAL.Repositories;
using TEFLY.DAL.Repositories.Interfaces;
using TEFLY.Mapping;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAutoMapper(
    typeof(MappingProfile),
    typeof(ViewModelMappingProfile)
);


// ── Database ──────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Identity (ONE registration only) ─────────────────────────
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// ── BLL Services ──────────────────────────────────────────────
builder.Services.AddScoped<IChildService, ChildService>();
builder.Services.AddScoped<IVaccineService, VaccineService>();
builder.Services.AddScoped<IVaccineSideEffectService, VaccineSideEffectService>();
builder.Services.AddScoped<IVaccinationScheduleService, VaccinationScheduleService>();
builder.Services.AddScoped<IHealthcareProviderService, HealthcareProviderService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IVaccinationRecordService, VaccinationRecordService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IAdverseReactionService, AdverseReactionService>();
builder.Services.AddScoped<IVaccineInventoryService, VaccineInventoryService>();
builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<IAwarenessService, AwarenessService>();

// ── MVC + Razor Pages ─────────────────────────────────────────
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedRolesAsync(services);
    await DataSeeder.SeedAdminAsync(services);
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    foreach (var role in new[] { "Admin", "Parent", "HealthcareProvider" })
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DataSeeder.SeedAsync(db);
}

app.Run();