using BaltaIoChallenge.WebApi.Extensions.v1;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using BaltaIoChallenge.WebApi.Repository.v1.Implementations;
using BaltaIoChallenge.WebApi.Services.v1.Auth.Contracts;
using BaltaIoChallenge.WebApi.Services.v1.Auth.Implementations.Login;
using BaltaIoChallenge.WebApi.Services.v1.Auth.Implementations.Register;
using BaltaIoChallenge.WebApi.Services.v1.Localization.Contracts;
using BaltaIoChallenge.WebApi.Services.v1.Localization.Implementations.LocationManagement;
using BaltaIoChallenge.WebApi.Services.v1.Localization.Implementations.SearchLocalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace BaltaIoChallenge.WebApi.Extensions.v1
{
    public static class BuilderExtensions
    {
        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JwtApiKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization();
        }

        public static void AddAuthServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddTransient<Services.v1.Token.TokenHandler>();
        }

        public static void AddLocalizationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILocalizationRepository, LocalizationRepository>();
            builder.Services.AddScoped<ISearchLocalizationService, SearchLocalizationService>();
            builder.Services.AddScoped<ILocalizationManagementService, LocalizationManagementService>();
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "API de localização",
                    Version = "v1",
                    Description = "Encontre e adicione localizações do Brasil",
                    Contact = new OpenApiContact
                    {
                        Name = "Repositório do projeto",
                        Url = new Uri("https://github.com/yThiagoFS/balta.io_challenge")
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token de autorização, exemplo: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                    Name = "Autorização",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);   
            });
        }
    }
}
