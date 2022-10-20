using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rotativa.AspNetCore;
using SCGP.COA.COMMON;
using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Extensions;
using SCGP.COA.DATAACCESS.Contexts;
using SCGP.COA.DATAACCESS.Infrastructures;
using SixLabors.ImageSharp;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using Wkhtmltopdf.NetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var envVar = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{envVar}.json", true, true);

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
var commandTimeout = builder.Configuration["CommandTimeout"];
int timeoutValue = 30;
if (commandTimeout != null)
    timeoutValue = Convert.ToInt32(commandTimeout);

#region database


builder.Services.AddDbContext<DbDataContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DbDataContext"), opt => opt.CommandTimeout(timeoutValue)));

builder.Services.AddDbContext<DbReadDataContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DbReadDataContext"), opt => opt.CommandTimeout(timeoutValue)));

builder.Services.AddDbContext<CoaSkicPM1to3Context>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaSkicPM1to3Context"), opt => opt.CommandTimeout(timeoutValue)));
builder.Services.AddDbContext<CoaSkicPM17GypsumContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaSkicPM17GypsumContext"), opt => opt.CommandTimeout(timeoutValue)));
builder.Services.AddDbContext<CoaSkicPM17KraftContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaSkicPM17KraftContext"), opt => opt.CommandTimeout(timeoutValue)));
builder.Services.AddDbContext<CoaSkicPM4to7Context>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaSkicPM4to7Context"), opt => opt.CommandTimeout(timeoutValue)));
builder.Services.AddDbContext<CoaSkicPM8GypsumContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaSkicPM8GypsumContext"), opt => opt.CommandTimeout(timeoutValue)));
builder.Services.AddDbContext<CoaSkicPM9Context>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaSkicPM9Context"), opt => opt.CommandTimeout(timeoutValue)));
builder.Services.AddDbContext<CoaTcpKaContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaTcpKaContext"), opt => opt.CommandTimeout(timeoutValue)));
builder.Services.AddDbContext<CoaTcpPcContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("CoaTcpPcContext"), opt => opt.CommandTimeout(timeoutValue)));

#endregion

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API-SCGP-COA, ENV :" + (appSettings?.Environment ?? "-"),
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "ITONE Co.,Ltd",
        },
    });

    c.IgnoreObsoleteProperties();


    #region Register Authentication


    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header \"Authorization: Bearer {token}\"",
        Name = "authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer",
        }
    };
    c.AddSecurityDefinition("Bearer", securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } },
                });
    #endregion Register Authentication


});
builder.Services.AddControllersWithViews();
builder.Services.AddWkhtmltopdf("Rotativa");
builder.Services.AddSingleton(appSettings);
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddCors(o => o.AddPolicy("CoaCorsPolicy", builder =>
{
    builder.WithOrigins(appSettings.AllowedHosts)
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

#region Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = appSettings.JwtIssuer,
            ValidAudience = appSettings.JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtKey)),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });
#endregion Authentication

var app = builder.Build();
IWebHostEnvironment env = app.Environment;
RotativaConfiguration.Setup(env.ContentRootPath);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger(a =>
{
    a.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers.Add(new OpenApiServer { Url = $"{appSettings.ApiUrl}" });
    });
});
app.UseSwaggerUI();
//}
app.UseCors("CoaCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();