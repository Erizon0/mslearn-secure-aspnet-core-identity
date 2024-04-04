using Microsoft.EntityFrameworkCore;
using RazorPagesPizza.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RazorPagesPizzaAuthConnection")
                       ?? throw new InvalidOperationException(
                           "Connection string 'RazorPagesPizzaAuthConnection' not found.");

builder.Services.AddDbContext<RazorPagesPizzaAuth>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<RazorPagesPizzaUser>(options => options.SignIn.RequireConfirmedAccount = false)
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<RazorPagesPizzaAuth>();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
    options.AddPolicy("Admin",      policy => { policy.RequireRole("SuperAdmin", "Admin"); });
    options.AddPolicy("Expedition", policy => { policy.RequireRole("SuperAdmin", "Admin", "Expedition"); });
    options.AddPolicy("User",       policy => { policy.RequireRole("SuperAdmin", "Admin", "Expedition", "User"); });
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope()) {
    // var userManager = scope.ServiceProvider.GetRequiredService<UserManager<RazorPagesPizzaUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles       = new[] { "SuperAdmin", "Admin", "Expedition", "User" };

    foreach (string role in roles) {
        if (!await roleManager.RoleExistsAsync(role)) {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();