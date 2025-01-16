using DAL.Contexts;
using Logic.IRepositories;
using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
	private readonly UserDbContext _context;
	public UserRepository(UserDbContext context)
	{
		_context = context;
	}
	public async Task<User> GetAsync(Guid userId)
	{
		return await _context.Users
			.FirstOrDefaultAsync(u => u.Id == userId);
	}

	public async Task<User> GetByNameAsync(string userName)
	{
		return await _context.Users
			.FirstOrDefaultAsync(u => u.Username.ToLower() == userName.ToLower());
	}

	public async Task<User> GetByEmailAsync(string email)
	{
		return await _context.Users
			.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
	}

	public async Task CreateAsync(User user)
	{
		if (user == null)
			throw new ArgumentNullException(nameof(user));

		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(User user)
	{
		if (user == null)
			throw new ArgumentNullException(nameof(user));

		var existingUser = await _context.Users.FindAsync(user.Id);
		if (existingUser != null)
		{
			existingUser.Username = user.Username;
			existingUser.Email = user.Email;

			_context.Users.Update(existingUser);
			await _context.SaveChangesAsync();
		}
		else
		{
			throw new KeyNotFoundException($"User with Id {user.Id} not found.");
		}
	}

	public async Task DeleteAsync(Guid userId)
	{
		var user = await _context.Users.FindAsync(userId);
		if (user != null)
		{
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}
		else
		{
			throw new KeyNotFoundException($"User with Id {userId} not found.");
		}
	}
}