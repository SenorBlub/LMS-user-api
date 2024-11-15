using Logic.Models;

namespace Logic.IRepositories;

public interface IUserContentRepository
{
	public Task<List<Guid>> GetContentAsync(Guid userId);
	public Task CreateAsync(UserContent userContent);
	public Task UpdateAsync(UserContent userContent);
	public Task DeleteAsync(Guid userId);
}