using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using DataAccess.UnitOfWork;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services.Mapper;
using Services.Models;
using Services.Services;
using Services.Services.Interfaces;
using HealthHubPro.Helper.Interfaces;
using HealthHubPro.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 

    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = null;
});
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<HealthHubDbContext>(options =>
    options.UseSqlServer(cs), ServiceLifetime.Scoped);

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBoundaryLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddScoped<IRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IRepository<Allergy>, AllergyRepository>();
builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
builder.Services.AddScoped<IRepository<EmergencyContact>, EmergencyContactRepository>();
builder.Services.AddScoped<IRepository<HealthcareProvider>, HealthcareProviderRepository>();
builder.Services.AddScoped<IRepository<Password>, PasswordRepository>();
builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();
builder.Services.AddScoped<IRepository<Permission>, PermissionRepository>();
builder.Services.AddScoped<IRepository<Person>, PersonRepository>();
builder.Services.AddScoped<IRepository<PrescriptionHistory>, PrescriptionHistoryRepository>();
builder.Services.AddScoped<IRepository<Prescription>, PrescriptionRepository>();
builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IRepository<Specialty>, SpecialtyRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IUserSessionProvider, UserSessionProvider>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();