using GoSakaryaApp.Business.DataProtection;
using GoSakaryaApp.Business.Operations.Area;
using GoSakaryaApp.Business.Operations.Comment;
using GoSakaryaApp.Business.Operations.Event;
using GoSakaryaApp.Business.Operations.EvenTicket;
using GoSakaryaApp.Business.Operations.Setting;
using GoSakaryaApp.Business.Operations.User;
using GoSakaryaApp.Data.Context;
using GoSakaryaApp.Data.Repositories;
using GoSakaryaApp.Data.UnitOfWork;
using GoSakaryaApp.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Jwt Token butonu a�ma. Swagger UI'da JWT Bearer token yetkilendirmesi ekler
builder.Services.AddSwaggerGen(options=>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer Token or Texbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() }
    });
});

// JWT Bearer Authentication ekler. Startup esnas�nda JWT do�rulama ayarlar� yap�l�r
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,    // s�resi dolan token� kabul etme.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))

        };
    });

// PROTECTION I�LEMLERI
builder.Services.AddScoped<IDataProtection, DataProtection>();  //SERVICE LIFETIMES
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
// Anahtarlar�n saklanaca�� dizin.
builder.Services.AddDataProtection().SetApplicationName("GoSakaryaApp").PersistKeysToFileSystem(keysDirectory);

// SERVICE LIFETIME
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));    // generic oldu�u i�in typeof
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IAreaService, AreaManager>();
builder.Services.AddScoped<IEventService , EventManager>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ISettingService, SettingManager>();
builder.Services.AddScoped<IEventTicketService, EventTicketManager>();

// Veritaban� ba�lant�s�.
var cs = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<GoSakaryaAppDbContext>(options => options.UseSqlServer(cs));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMaintenanceMode();   // Bak�m modu middleware'�n� ekler

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
