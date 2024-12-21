using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.Abstraction.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inforce.UrlShortener.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();

            services.AddControllers();

            // CORS configuration
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidIssuer = Configuration ["JwtSettings:Issuer"],
                            ValidAudience = Configuration ["JwtSettings:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration ["JwtSettings:SecretKey"]))
                        };
                    });

            // SQL Server configuration
            services.AddDbContext<Inforce.UrlShortener.DAL.Data.UrlShortenerContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("UrlShortener"))
                       .UseLazyLoadingProxies());
            services.AddScoped<IUnitOfWork, Inforce.UrlShortener.DAL.Data.UnitOfWork>();

            services.AddScoped<IUserService, Inforce.UrlShortener.BLL.Services.UserService>();
            services.AddScoped<IUrlService, Inforce.UrlShortener.BLL.Services.UrlService>();

            services.AddAutoMapper(typeof(Inforce.UrlShortener.BLL.AutomapperProfile).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inforce.UrlShortener API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inforce.UrlShortener API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Use CORS
            app.UseCors("AllowAnyOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
