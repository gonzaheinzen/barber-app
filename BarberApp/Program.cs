
using System.Text.Json.Serialization;
using BarberApp.EF;
using BarberApp.Interfaces;
using BarberApp.Repositories;
using BarberApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BarberApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("MYSQL_CONNECTION_STRING"),
                    new MySqlServerVersion(new Version(8, 0, 32)),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(3) // 3 intentos
                )
            );

            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IBarberRepository, BarberRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IBarberService, BarberService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                    p.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());
            });



            var app = builder.Build();

            app.UseCors("AllowAll");



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
}
