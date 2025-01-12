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
			entity.ToTable("Users");
			entity.HasKey(u => u.Id);

			entity.Property(u => u.Id)
				.HasColumnName("Id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(u => u.Username)
				.HasColumnName("Username")
				.HasColumnType("varchar(255)")
				.IsRequired();

			entity.Property(u => u.Email)
				.HasColumnName("Email")
				.HasColumnType("varchar(255)")
				.IsRequired();

			entity.Property(u => u.Password)
				.HasColumnName("Password")
				.HasColumnType("varchar(255)")
				.IsRequired();
		});

		// UserPlan Entity
		modelBuilder.Entity<UserPlan>(entity =>
		{
			entity.ToTable("UserPlans");
			entity.HasKey(up => up.Id);

			entity.Property(up => up.Id)
				.HasColumnName("Id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(up => up.UserId)
				.HasColumnName("UserId")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(up => up.PlanId)
				.HasColumnName("PlanId")
				.HasColumnType("char(36)")
				.IsRequired();
		});

		// UserActivity Entity
		modelBuilder.Entity<UserActivity>(entity =>
		{
			entity.ToTable("UserActivities");
			entity.HasKey(ua => ua.Id);

			entity.Property(ua => ua.Id)
				.HasColumnName("Id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(ua => ua.UserId)
				.HasColumnName("UserId")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(ua => ua.ActivityId)
				.HasColumnName("ActivityId")
				.HasColumnType("char(36)")
				.IsRequired();
		});

		// UserContent Entity
		modelBuilder.Entity<UserContent>(entity =>
		{
			entity.ToTable("UserContents");
			entity.HasKey(uc => uc.Id);

			entity.Property(uc => uc.Id)
				.HasColumnName("Id")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(uc => uc.UserId)
				.HasColumnName("UserId")
				.HasColumnType("char(36)")
				.IsRequired();

			entity.Property(uc => uc.ContentId)
				.HasColumnName("ContentId")
				.HasColumnType("char(36)")
				.IsRequired();
		});
	}
}
