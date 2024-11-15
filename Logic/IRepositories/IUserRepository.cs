using Logic.Models;

namespace Logic.IRepositories;

public interface IUserRepository
{
	public Task<User> GetAsync(Guid userId);
	public Task<User> GetByNameAsync(string userName);
	public Task<User> GetByEmailAsync(string email);
	public Task CreateAsync(User user);
	public Task UpdateAsync(User user);
	public Task DeleteAsync(Guid userId);
}