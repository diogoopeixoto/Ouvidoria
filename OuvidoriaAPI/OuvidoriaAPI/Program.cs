using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OuvidoriaAPI.Data;
using OuvidoriaAPI.Service;

namespace OuvidoriaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string servidorEmail = builder.Configuration.GetSection("ServidorEmail").Value;
            int portaEmail = int.Parse(builder.Configuration.GetSection("Porta").Value);
            string usrEmail = builder.Configuration.GetSection("UsuarioEmail").Value;
            string senhaEmail = builder.Configuration.GetSection("SenhaEmail").Value;
            bool ssl = bool.Parse(builder.Configuration.GetSection("SSL").Value);

            string usrDb = builder.Configuration.GetSection("UsrDB").Value;
            string senhaDb = builder.Configuration.GetSection("SenhaDB").Value;

            EmailService.Configure(servidorEmail, ssl, portaEmail, usrEmail, senhaEmail);
            OuvidoriaContext.ConfiguraDb(usrDb, senhaDb);

            using (OuvidoriaContext db = new OuvidoriaContext())
            {
                db.Database.Migrate();
                FakeDB.Create();
            }

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var key = Encoding.ASCII.GetBytes("O98I7UYWE9R8TY4HG5F6DS3X2CV1BHJ9H8GF5TG2YH");
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddCors(o => o.AddPolicy("OuvidoriaAPI", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}