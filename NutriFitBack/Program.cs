using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NutriFit.Application.Input.Repositories;
using NutriFit.Application.Output.Interfaces;
using InputFactory = NutriFit.Infrastructure.Input.Factory;
using OutputFactory = NutriFit.Infrastructure.Output.Factory;
using NutriFit.Infrastructure.Input.Repositories;
using NutriFit.Infrastructure.Output.Repositories;
using NutriFitBack;
using System.Text;
using System.Text.Json.Serialization;
using NutriFit.Application.Input.Receivers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<InputFactory.SqlFactory>();
builder.Services.AddScoped<OutputFactory.SqlFactory>();

builder.Services.AddTransient<IWriteUserRepository, WriteUserRepository>();
builder.Services.AddTransient<IReadUserRepository, ReadUserRepository>();
builder.Services.AddTransient<InsertUserReceiver>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
var AcceptOrigins = "AcceptOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AcceptOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NutriFit - Backend", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
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
            Array.Empty<string>()
        }
    });
});
var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AcceptOrigins);


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
