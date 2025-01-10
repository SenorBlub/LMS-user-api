using Logic.IRepositories;
using Logic.IServices;
using Logic.Models;
using Microsoft.AspNetCore.Identity;

namespace Logic.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IUserPlanRepository _userPlanRepository;
	private readonly IUserActivityRepository _userActivityRepository;
	private readonly IUserContentRepository _userContentRepository;
	private readonly IPasswordHasher<User> _passwordHasher;

	public UserService(IUserRepository userRepository,
		IUserPlanRepository userPlanRepository,
		IUserActivityRepository userActivityRepository,
		IUserContentRepository userContentRepository,
		IPasswordHasher<User> passwordHasher)
	{
		_userRepository = userRepository;
		_userPlanRepository = userPlanRepository;
		_userActivityRepository = userActivityRepository;
		_userContentRepository = userContentRepository;
		_passwordHasher = passwordHasher;
	}

	public async Task<User> GetAsync(Guid userId)
	{

		User user = await _userRepository.GetAsync(userId);

		if (user == null)
			throw new NullReferenceException($"User with userId {userId} was not found.");

		return user;
	}

	public async Task<User> GetByNameAsync(string userName)
	{
		User user = await _userRepository.GetByNameAsync(userName);

		if (user == null)
			throw new NullReferenceException($"User with Username {userName} was not found.");

		return user;
	}

	public async Task<User> GetByEmailAsync(string email)
	{
		User user = await _userRepository.GetByEmailAsync(email);
		if (user == null)
		{
			Console.WriteLine($"User with email {email} was not found.");
		}

		return user;
	}

	public async Task UpdateAsync(Guid userId, User user)
	{
		User existingUser = await _userRepository.GetAsync(userId);
		if (existingUser == null)
			throw new KeyNotFoundException($"User with Id {userId} was not found.");

		existingUser.Username = user.Username;
		existingUser.Email = user.Email;

		await _userRepository.UpdateAsync(existingUser);
	}

	public async Task UpdateAsync(string userName, User user)
	{
		User existingUser = await _userRepository.GetByNameAsync(userName);
		if (existingUser == null)
			throw new KeyNotFoundException($"User with Username {userName} was not found.");

		existingUser.Username = user.Username;
		existingUser.Email = user.Email;

		await _userRepository.UpdateAsync(existingUser);
	}

	public async Task DeleteAsync(Guid userId)
	{
		await _userRepository.DeleteAsync(userId);
	}

	public async Task DeleteAsync(string userName)
	{
		User user = await _userRepository.GetByNameAsync(userName);
		if (user == null)
			throw new KeyNotFoundException($"User with Username {userName} was not found.");

		await _userRepository.DeleteAsync(user.Id);
	}

	public async Task CreateAsync(User user)
	{
		var existingUser = await GetByEmailAsync(user.Email);
		if (existingUser != null) throw new InvalidOperationException("Email already in use");

		user.Password = _passwordHasher.HashPassword(user, user.Password);
		await _userRepository.CreateAsync(user);
	}

	public async Task<List<Guid>> GetActivitiesAsync(Guid userId)
	{
		return await _userActivityRepository.GetActivitiesAsync(userId);
	}

	public async Task<List<Guid>> GetActivitiesByNameAsync(string userName)
	{
		User user = await _userRepository.GetByNameAsync(userName);
		if (user == null)
			throw new KeyNotFoundException($"User with Username {userName} was not found.");

		return await _userActivityRepository.GetActivitiesAsync(user.Id);
	}

	public async Task<List<Guid>> GetContentAsync(Guid userId)
	{
		return await _userContentRepository.GetContentAsync(userId);
	}

	public async Task<List<Guid>> GetContentByNameAsync(string userName)
	{
		User user = await _userRepository.GetByNameAsync(userName);
		if (user == null)
			throw new KeyNotFoundException($"User with Username {userName} was not found.");

		return await _userContentRepository.GetContentAsync(user.Id);
	}

	public async Task<List<Guid>> GetPlansAsync(Guid userId)
	{
		return await _userPlanRepository.GetPlansAsync(userId);
	}

	public async Task<List<Guid>> GetPlansByNameAsync(string userName)
	{
		User user = await _userRepository.GetByNameAsync(userName);
		if (user == null)
			throw new KeyNotFoundException($"User with Username {userName} was not found.");

		return await _userPlanRepository.GetPlansAsync(user.Id);
	}

	public async Task ConnectUserPlanAsync(Guid userId, Guid planId)
	{
		var userPlan = new UserPlan
		{
			Id = Guid.NewGuid(),
			UserId = userId,
			PlanId = planId
		};
		await _userPlanRepository.CreateAsync(userPlan);
	}

	public async Task DisconnectUserPlanAsync(Guid userId, Guid planId)
	{
		await _userPlanRepository.DeleteAsync(userId);
	}

	public async Task ConnectUserActivityAsync(Guid userId, Guid activityId)
	{
		var userActivity = new UserActivity
		{
			Id = Guid.NewGuid(),
			UserId = userId,
			ActivityId = activityId
		};
		await _userActivityRepository.CreateAsync(userActivity);
	}

	public async Task DisconnectUserActivityAsync(Guid userId, Guid activityId)
	{
		await _userActivityRepository.DeleteAsync(userId);
	}

	public async Task ConnectUserContentAsync(Guid userId, Guid contentId)
	{
		var userContent = new UserContent
		{
			Id = Guid.NewGuid(),
			UserId = userId,
			ContentId = contentId
		};
		await _userContentRepository.CreateAsync(userContent);
	}

	public async Task DisconnectUserContentAsync(Guid userId, Guid contentId)
	{
		await _userContentRepository.DeleteAsync(userId);
	}

	public async Task<bool> LoginUserAsync(string email, string password)
	{
		User user = GetByEmailAsync(email).Result;
		var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
		if (result != PasswordVerificationResult.Success)
			return false;
		return true;
	}

	public async Task<bool> LoginUserAsync(Guid UserId, string Password)
	{
		User user = GetAsync(UserId).Result;
		var result = _passwordHasher.VerifyHashedPassword(user, user.Password, Password);
		if(result != PasswordVerificationResult.Success)
			return false;
		return true;
	}
}