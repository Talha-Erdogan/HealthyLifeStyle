using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Services;
using HealthyLifeStyle.Types.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//yeni servsler eklenebilir.
builder.Services.AddScoped<IBloodGroupService, BloodGroupService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INeedForBloodService, NeedForBloodService>();



var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<HealthyLifeStyleDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(60); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
