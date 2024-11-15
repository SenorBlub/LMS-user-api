using DAL.Contexts;
using Logic.IRepositories;
using Logic.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserActivityRepository : IUserActivityRepository
{
	private readonly UserDbContext _context;
	public UserActivityRepository(UserDbContext context)
	{
		_context = context;
	}
	public async Task<List<Guid>> GetActivitiesAsync(Guid userId)
	{
		return await _context.UserActivities
			.Where(ua => ua.UserId == userId)
			.Select(ua => ua.ActivityId)
			.ToListAsync();
	}

	public async Task CreateAsync(UserActivity userActivity)
	{
		if (userActivity == null)
			throw new ArgumentNullException(nameof(userActivity));
		await _context.UserActivities.AddAsync(userActivity);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(UserActivity userActivity)
	{
		if (userActivity == null)
			throw new ArgumentNullException(nameof(userActivity));
		var existingActivity = await _context.UserActivities.FindAsync(userActivity.Id);
		if (existingActivity != null)
		{
			existingActivity.ActivityId = userActivity.ActivityId; 
			_context.UserActivities.Update(existingActivity);
			await _context.SaveChangesAsync();
		}
		else
		{
			throw new KeyNotFoundException($"UserActivity with Id {userActivity.Id} not found.");
		}
	}

	public async Task DeleteAsync(Guid userId)
	{
		var userActivities = await _context.UserActivities
			.Where(ua => ua.UserId == userId)
			.ToListAsync();
		if (userActivities.Any())
		{
			_context.UserActivities.RemoveRange(userActivities);
			await _context.SaveChangesAsync();
		}
	}
}