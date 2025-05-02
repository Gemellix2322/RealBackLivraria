
using System.Configuration;
using back_teste.Data;
using back_teste.Repositorios;
using back_teste.Repositorios.Interfaces;
using BackLivrariaTeste.Repositorios;
using BackLivrariaTeste.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_teste
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

            builder.Services.AddDbContext<LivrariaDBContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ConnectionString"),
                    ServerVersion.Parse("10.4.32-MariaDB"),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure()
                )
            );

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                // Configure para escutar em todas as interfaces
                serverOptions.ListenAnyIP(
                    // Use a porta fornecida pelo ambiente ou 5000 como padrão
                    int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "5000")
                );
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            builder.Services.AddScoped<ILivrosRepositorio, LivrosRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
