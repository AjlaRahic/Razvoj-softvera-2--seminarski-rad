using AutoMapper;
using eFood;
using eFood.API;
using eFood.Services;
using eFood.Services.Database;
using eFood.Services.NarudzbeStateMachine;
using eFood.Services.Reports;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient; // koristi pravi provider
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add scoped services for business logic
builder.Services.AddScoped<IDrzavaService, DrzavaService>();
builder.Services.AddScoped<IDojmoviService, DojmoviService>();
builder.Services.AddScoped<IGradService, GradService>();
builder.Services.AddScoped<IKorisniciService, KorisniciService>();
builder.Services.AddScoped<IUlogaService, UlogaService>();
builder.Services.AddScoped<IKategorijaService, KategorijaService>();
builder.Services.AddScoped<IJeloService, JeloService>();
builder.Services.AddScoped<INarudzbaService, NarudzbaService>();
builder.Services.AddScoped<IStatusNarudzbeService, StatusNarudzbeService>();
builder.Services.AddScoped<IStavkeNarudzbeService, StavkeNarudzbeService>();
builder.Services.AddScoped<IUplataService, UplataService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IRestoranService, RestoranService>();
builder.Services.AddScoped<IKorisniciUloga, KorisniciUlogaService>();
builder.Services.AddScoped<IKorpaService, KorpaService>();
builder.Services.AddScoped<IPriloziService, PriloziService>();

// Add state machine scoped services for order states
builder.Services.AddScoped<BaseNarudzbaState>();
builder.Services.AddScoped<InitialNarudzbaState>();
builder.Services.AddScoped<DraftNarudzbeState>();
builder.Services.AddScoped<ActiveNarudzbaState>();

// Configure JSON serialization with camel case for consistency
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Swagger and OpenAPI documentation setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Basic"
                }
            },
            new string[]{}
        }
    });
});

// Connection string setup, fallbacks to environment variables
var cs = builder.Configuration.GetConnectionString("Default") ??
         builder.Configuration.GetConnectionString("DefaultConnection") ??
         Environment.GetEnvironmentVariable("ConnectionStrings__Default") ??
         Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

Console.WriteLine("Connection string: " + cs);

// Database context registration with SqlServer
builder.Services.AddDbContext<EFoodContext>(options =>
    options.UseSqlServer(cs, sql =>
    {
        sql.EnableRetryOnFailure();
        sql.CommandTimeout(30);
    })
);

// Register AutoMapper to handle mappings
builder.Services.AddAutoMapper(typeof(IKorisniciService).Assembly);

// Authentication setup for Basic Authentication
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();

// Development environment setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  // Ensure HTTPS is used
app.UseStaticFiles();       // To serve static files

// Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map the controllers
app.MapControllers();
Console.WriteLine(">>> POKRECEM SEED <<<");

// Database migration at startup (ensure database is up to date)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<EFoodContext>();
    try
    {
        dataContext.Database.Migrate();  // Apply any pending migrations

        // Pozivanje DBSeeder da doda dinamične podatke
        DBSeeder.SeedDatabase(dataContext);
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB migration or seeding failed: " + ex.Message);  // Log migration errors
    }
}

app.Run();  // Start the application
