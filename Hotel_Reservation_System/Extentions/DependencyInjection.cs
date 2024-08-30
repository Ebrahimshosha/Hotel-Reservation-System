﻿using Hotel_Reservation_System.Profiles.OfferProfile;
using Hotel_Reservation_System.Profiles.Roomprofiles;
using Hotel_Reservation_System.Services.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Hotel_Reservation_System.Extentions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddAuthConfig(configuration);


        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(connectionString)
                .UseLoggerFactory(MyLoggerFactory)
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        });


        var IdentityConnectionString = configuration.GetConnectionString("IdentityConnection") ??
           throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");

        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseSqlServer(IdentityConnectionString);
        });

        services.AddSwaggerServices();
        services.AddMapperConfig();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    private static IServiceCollection AddMapperConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(RoomProfile));
        services.AddAutoMapper(typeof(OfferProfile));

        return services;
    }
    private static IServiceCollection AddAuthConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false; 
            options.Password.RequiredLength = 8; 
            options.Password.RequiredUniqueChars = 1; 
        })
                .AddEntityFrameworkStores<AppIdentityDbContext>();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience
            };
        });

        return services;
    }
}
