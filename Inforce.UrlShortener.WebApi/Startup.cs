using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.Abstraction.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            services.AddControllers();

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
