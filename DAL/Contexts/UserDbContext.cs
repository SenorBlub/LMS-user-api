using Logic.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts;

public class UserDbContext : DbContext
{
	public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<UserPlan> UserPlans { get; set; }
	public DbSet<UserActivity> UserActivities { get; set; }
	public DbSet<UserContent> UserContents { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// User Entity
		modelBuilder.Entity<User>(entity =>
		{
			entity.ToTable("Users");
			entity.HasKey(u => u.Id);

			entity.Property(u => u.Id)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();

			entity.Property(u => u.Username)
				.HasColumnType("VARCHAR(255)")
				.IsRequired();

			entity.Property(u => u.Email)
				.HasColumnType("VARCHAR(255)")
				.IsRequired();
			
			entity.Property(u => u.Password)
				.HasColumnType("VARCHAR(255)")
				.IsRequired();

			// Relationships
			entity.HasMany(u => u.UserPlans)
				.WithOne()
				.HasForeignKey(up => up.UserId)
				.IsRequired(false);

			entity.HasMany(u => u.UserActivities)
				.WithOne()
				.HasForeignKey(ua => ua.UserId)
				.IsRequired(false);

			entity.HasMany(u => u.UserContents)
				.WithOne()
				.HasForeignKey(uc => uc.UserId)
				.IsRequired(false);
		});

		// UserPlan Entity
		modelBuilder.Entity<UserPlan>(entity =>
		{
			entity.ToTable("UserPlan");
			entity.HasKey(up => up.Id);

			entity.Property(up => up.Id)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();

			entity.Property(up => up.UserId)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();

			entity.Property(up => up.PlanId)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();
		});

		// UserActivity Entity
		modelBuilder.Entity<UserActivity>(entity =>
		{
			entity.ToTable("UserActivity");
			entity.HasKey(ua => ua.Id);

			entity.Property(ua => ua.Id)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();

			entity.Property(ua => ua.UserId)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();

			entity.Property(ua => ua.ActivityId)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();
		});

		// UserContent Entity
		modelBuilder.Entity<UserContent>(entity =>
		{
			entity.ToTable("UserContent");
			entity.HasKey(uc => uc.Id);

			entity.Property(uc => uc.Id)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();

			entity.Property(uc => uc.UserId)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();

			entity.Property(uc => uc.ContentId)
				.HasColumnType("VARCHAR(36)")
				.IsRequired();
		});
	}
}