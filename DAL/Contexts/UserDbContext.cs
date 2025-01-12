using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts;

public class UserDbContext : DbContext
{
	public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

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
			entity.ToTable("users");
			entity.HasKey(u => u.Id);

			entity.Property(u => u.Id)
				.HasColumnName("id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(u => u.Username)
				.HasColumnName("username")
				.HasColumnType("varchar(255)")
				.IsRequired();

			entity.Property(u => u.Email)
				.HasColumnName("email")
				.HasColumnType("varchar(255)")
				.IsRequired();

			entity.Property(u => u.Password)
				.HasColumnName("password")
				.HasColumnType("varchar(255)")
				.IsRequired();
		});

		// UserPlan Entity
		modelBuilder.Entity<UserPlan>(entity =>
		{
			entity.ToTable("user_plans");
			entity.HasKey(up => up.Id);

			entity.Property(up => up.Id)
				.HasColumnName("id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(up => up.UserId)
				.HasColumnName("user_id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(up => up.PlanId)
				.HasColumnName("plan_id")
				.HasColumnType("char(36)")
				.IsRequired();
		});

		// UserActivity Entity
		modelBuilder.Entity<UserActivity>(entity =>
		{
			entity.ToTable("user_activities");
			entity.HasKey(ua => ua.Id);

			entity.Property(ua => ua.Id)
				.HasColumnName("id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(ua => ua.UserId)
				.HasColumnName("user_id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(ua => ua.ActivityId)
				.HasColumnName("activity_id")
				.HasColumnType("char(36)")
				.IsRequired();
		});

		// UserContent Entity
		modelBuilder.Entity<UserContent>(entity =>
		{
			entity.ToTable("user_contents");
			entity.HasKey(uc => uc.Id);

			entity.Property(uc => uc.Id)
				.HasColumnName("id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(uc => uc.UserId)
				.HasColumnName("user_id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(uc => uc.ContentId)
				.HasColumnName("content_id")
				.HasColumnType("char(36)")
				.IsRequired();
		});
	}
}
