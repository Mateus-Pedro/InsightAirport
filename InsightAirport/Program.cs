using InsightAirport.Data;
using InsightAirport.Repositories;
using InsightAirport.Repositories.Interfaces;
using InsightAirport.Services;
using InsightAirport.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightAirport
{
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

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });


            builder.Services.AddEntityFrameworkSqlServer()
                   .AddDbContext<InsightAirportDBContext>(
                         options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                    );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                                    builder => builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader());

            });

            builder.Services.AddLogging();
            builder.Services.AddAutoMapper(typeof(Program));

            //Repositories
            builder.Services.AddScoped<ICommunicationLogRepository, CommunicationLogRepository>();
            builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();
            builder.Services.AddScoped<IPilotRepository, PilotRepository>();

            //Services
            builder.Services.AddScoped<IAirplaneService, AirplaneService>();
            builder.Services.AddScoped<IPilotService, PilotService>();
            builder.Services.AddScoped<ICommunicationLogService, CommunicationLogService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}