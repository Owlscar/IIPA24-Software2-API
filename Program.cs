using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Software2_API.Dtos;
using Software2_API.Repositories;
using Software2_API.Repositories.Models;
using Software2_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Jwt Setings
var bindJwtSettings = new JwtSettingsDto();
builder.Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
builder.Services.AddSingleton(bindJwtSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
        ValidateIssuer = bindJwtSettings.ValidateIssuer,
        ValidIssuer = bindJwtSettings.ValidIssuer,
        ValidateAudience = bindJwtSettings.ValidateAudience,
        ValidAudience = bindJwtSettings.ValidAudience,
        RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
        ValidateLifetime = bindJwtSettings.RequireExpirationTime,
        ClockSkew = TimeSpan.Zero,
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired-Time", "true");
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddControllers();
builder.Services.AddDbContext<TestContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnetionString:Test")));
//Add Repositories
builder.Services.AddScoped<UserRepository, UserRepository>();
//Add Services
builder.Services.AddScoped<UserService, UserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                Array.Empty<string>()
        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Software2_API : " + builder.Configuration.GetValue<string>("Application:Environment"),
        Description = "API con implementacion de JWT para la comunicación entre la capa visual y la capa de negocio.",
        Contact = new OpenApiContact
        {
            Name = "API Desarrollada por Oscar Gomez",
            Url = new Uri("https://www.google.com/")
        },
        License = new OpenApiLicense
        {
            Name = "Documentación Anexa",
            Url = new Uri("https://www.google.com/")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());// global cors policy WithOrigins("https://localhost:8185")

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
