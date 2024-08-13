
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Hotel_Reservation_System.AutoFac;
using Hotel_Reservation_System.Data;
using Hotel_Reservation_System.Helpers;
using Hotel_Reservation_System.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hotel_Reservation_System;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        builder.Services.AddDbContext<StoreContext>(Options =>
        {
            Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
              .UseLoggerFactory(MyLoggerFactory)
              .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        });

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            builder.RegisterModule(new AutoFacModule()));

        var app = builder.Build();

        app.UseMiddleware<TransactionMiddleware>();

        MapperHelper.Mapper = app.Services.GetService<IMapper>()!;

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
