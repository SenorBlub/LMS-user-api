namespace Logic.Models;

public class User
{
	public Guid Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }

	public string Password { get; set; }

	public ICollection<UserPlan> UserPlans { get; set; }
	public ICollection<UserActivity> UserActivities { get; set; }
	public ICollection<UserContent> UserContents { get; set; }
}