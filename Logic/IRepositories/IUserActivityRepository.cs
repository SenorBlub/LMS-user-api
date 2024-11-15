using Logic.Models;

namespace Logic.IRepositories;

public interface IUserActivityRepository
{
	public Task<List<Guid>> GetActivitiesAsync(Guid userId);
	public Task CreateAsync(UserActivity userActivity);
	public Task UpdateAsync(UserActivity userActivity);
	public Task DeleteAsync(Guid userId);
}