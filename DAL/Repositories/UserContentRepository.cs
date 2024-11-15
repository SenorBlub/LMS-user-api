using DAL.Contexts;
using Logic.IRepositories;
using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserContentRepository : IUserContentRepository
{
	private readonly UserDbContext _context;
	public UserContentRepository(UserDbContext context)
	{
		_context = context;
	}
	public async Task<List<Guid>> GetContentAsync(Guid userId)
	{
		return await _context.UserContents
			.Where(uc => uc.UserId == userId)
			.Select(uc => uc.ContentId)
			.ToListAsync();
	}

	public async Task CreateAsync(UserContent userContent)
	{
		if (userContent == null)
			throw new ArgumentNullException(nameof(userContent));
		await _context.UserContents.AddAsync(userContent);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(UserContent userContent)
	{
		if (userContent == null)
			throw new ArgumentNullException(nameof(userContent));
		var existingContent = await _context.UserContents.FindAsync(userContent.Id);
		if (existingContent != null)
		{
			existingContent.ContentId = userContent.ContentId; 
			_context.UserContents.Update(existingContent);
			await _context.SaveChangesAsync();
		}
		else
		{
			throw new KeyNotFoundException($"UserContent with Id {userContent.Id} not found.");
		}
	}

	public async Task DeleteAsync(Guid userId)
	{
		var userContents = await _context.UserContents
			.Where(uc => uc.UserId == userId)
			.ToListAsync();

		if (userContents.Any())
		{
			_context.UserContents.RemoveRange(userContents);
			await _context.SaveChangesAsync();
		}
	}
}