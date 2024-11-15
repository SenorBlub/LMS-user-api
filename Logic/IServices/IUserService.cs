using Logic.Models;

namespace Logic.IServices;

public interface IUserService
{
	public Task<User> GetAsync(Guid userId);
	public Task<User> GetByNameAsync(string userName);
	public Task<User> GetByEmailAsync(string email);
	public Task UpdateAsync(Guid userId, User user);
	public Task UpdateAsync(string userName, User user);
	public Task DeleteAsync(Guid userId);
	public Task DeleteAsync(string userName);
	public Task CreateAsync(User user);
	public Task<List<Guid>> GetActivitiesAsync(Guid userId);
	public Task<List<Guid>> GetActivitiesByNameAsync(string userName);
	public Task<List<Guid>> GetContentAsync(Guid userId);
	public Task<List<Guid>> GetContentByNameAsync(string userName);
	public Task<List<Guid>> GetPlansAsync(Guid userId);
	public Task<List<Guid>> GetPlansByNameAsync(string userName);
	public Task ConnectUserPlanAsync(Guid userId, Guid planId);
	public Task DisconnectUserPlanAsync(Guid userId, Guid planId);
	public Task ConnectUserActivityAsync(Guid userId, Guid activityId);
	public Task DisconnectUserActivityAsync(Guid userId, Guid activityId);
	public Task ConnectUserContentAsync(Guid userId, Guid contentId);
	public Task DisconnectUserContentAsync(Guid userId, Guid contentId);
	public Task<bool> LoginUserAsync(string email, string password);
	public Task<bool> LoginUserAsync(Guid userId, string password);
}