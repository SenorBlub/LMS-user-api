using DAL.Contexts;
using Logic.IRepositories;
using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserPlanRepository : IUserPlanRepository
{
	private readonly UserDbContext _context;
	public UserPlanRepository(UserDbContext context)
	{
		_context = context;
	}
	public async Task<List<Guid>> GetPlansAsync(Guid userId)
	{
		return await _context.UserPlans
			.Where(up => up.UserId == userId)
			.Select(up => up.PlanId)
			.ToListAsync();
	}

	public async Task CreateAsync(UserPlan userPlan)
	{
		if (userPlan == null)
			throw new ArgumentNullException(nameof(userPlan));
		await _context.UserPlans.AddAsync(userPlan);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(UserPlan userPlan)
	{
		if (userPlan == null)
			throw new ArgumentNullException(nameof(userPlan));
		var existingPlan = await _context.UserPlans.FindAsync(userPlan.Id);
		if (existingPlan != null)
		{
			existingPlan.PlanId = userPlan.PlanId;
			_context.UserPlans.Update(existingPlan);
			await _context.SaveChangesAsync();
		}
		else
		{
			throw new KeyNotFoundException($"UserPlan with Id {userPlan.Id} not found.");
		}
	}

	public async Task DeleteAsync(Guid userId)
	{
		var userPlans = await _context.UserPlans
			.Where(up => up.UserId == userId)
			.ToListAsync();

		if (userPlans.Any())
		{
			_context.UserPlans.RemoveRange(userPlans);
			await _context.SaveChangesAsync();
		}
	}
}