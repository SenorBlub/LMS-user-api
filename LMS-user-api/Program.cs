using DAL.Contexts;
using DAL.Repositories;
using Logic.IRepositories;
using Logic.IServices;
using Logic.Models;
using Logic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Env.Load();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(corsPolicyBuilder =>
	{
		corsPolicyBuilder.WithOrigins()
			.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials()
			.SetIsOriginAllowed(_ => true);
	});
});

var connectionString =
	$"Server={Env.GetString("DB_HOST")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_NAME")};User={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASSWORD")};";

Console.WriteLine(connectionString);

builder.Services.AddDbContext<UserDbContext>(options =>
	options.UseMySql(
		connectionString,
		new MySqlServerVersion(new Version(Env.GetInt("SQL_MAJOR"), Env.GetInt("SQL_MINOR"), Env.GetInt("SQL_BUILD"))),
		mySqlOptions => mySqlOptions.EnableRetryOnFailure()

	).LogTo(Console.WriteLine)
);

//!TODO auth

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserActivityRepository, UserActivityRepository>();
builder.Services.AddScoped<IUserPlanRepository, UserPlanRepository>();
builder.Services.AddScoped<IUserContentRepository, UserContentRepository>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
