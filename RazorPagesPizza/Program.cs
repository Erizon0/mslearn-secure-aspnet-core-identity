using Microsoft.EntityFrameworkCore;
using RazorPagesPizza.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RazorPagesPizzaAuthConnection")
                       ?? throw new InvalidOperationException(
                           "Connection string 'RazorPagesPizzaAuthConnection' not found.");

builder.Services.AddDbContext<RazorPagesPizzaAuth>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<RazorPagesPizzaUser>(options => options.SignIn.RequireConfirmedAccount = false)
       .AddEntityFrameworkStores<RazorPagesPizzaAuth>();


// builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
// {
//     microsoftOptions.ClientId     = builder.Configuration["Authentication:Microsoft:ClientId"];
//     microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
// });

// builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
//
// builder.Services.AddControllersWithViews(options => {
//     var policy = new AuthorizationPolicyBuilder()
//                  .RequireAuthenticatedUser()
//                  .Build();
//     options.Filters.Add(new AuthorizeFilter(policy));
// });
// builder.Services.AddRazorPages()
//        .AddMicrosoftIdentityUI();

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

app.Run();