using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Portfolio.Authentication;
using Portfolio.Data;
using Portfolio.Data.Configuration;
using Portfolio.Data.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddDbContext<AppDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));

builder.Services.AddTransient<CategoryService>()
                .AddTransient<PostService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<AuthenticationService>();




Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey: "Ngo9BigBOggjHTQxAR8/V1NAaF5cWWBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWX5ec3RWR2JZVU1zV0A=");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization(); 

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
