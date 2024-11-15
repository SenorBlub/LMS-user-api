using Logic.Models;

namespace Logic.IRepositories;

public interface IUserPlanRepository
{
	public Task<List<Guid>> GetPlansAsync(Guid userId);
	public Task CreateAsync(UserPlan userPlan);
	public Task UpdateAsync(UserPlan userPlan);
	public Task DeleteAsync(Guid userId);
}