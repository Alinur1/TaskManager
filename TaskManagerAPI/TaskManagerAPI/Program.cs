using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagerAPI.BLL.Interfaces;
using TaskManagerAPI.BLL.Services;
using TaskManagerAPI.DAL.TaskManagerDataContext;
using TaskManagerAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);

var connectionStrings = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStrings));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtConfig = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]))
        };
    });

// Add services to the container.
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<ITaskItem, TaskItemService>();
builder.Services.AddSingleton<JwtHelper>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React App
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
