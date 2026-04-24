
using Masar_Backend_v1.Services;
using Scalar.AspNetCore;

namespace Masar_Backend_v1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient<IMasarService, MasarService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.UseCors();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            //var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            //app.Run($"http://0.0.0.0:{port}");
            app.Run();
        }
    }
}
