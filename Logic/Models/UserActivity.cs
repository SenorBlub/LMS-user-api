namespace Logic.Models;

public class UserActivity
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid ActivityId { get; set; }
}