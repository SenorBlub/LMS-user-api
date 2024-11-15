﻿using Logic.IServices;
using Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_user_api.Controllers
{
	[Route("user/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> Get(Guid userId)
		{
			try
			{
				var user = await _userService.GetAsync(userId);
				return Ok(user);
			}
			catch (NullReferenceException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("by-name/{userName}")]
		public async Task<IActionResult> GetByName(string userName)
		{
			try
			{
				var user = await _userService.GetByNameAsync(userName);
				return Ok(user);
			}
			catch (NullReferenceException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] User user)
		{
			if (user == null)
			{
				return BadRequest("User object cannot be null.");
			}

			await _userService.CreateAsync(user);
			return CreatedAtAction(nameof(Get), new { userId = user.Id }, user);
		}

		[HttpPut("{userId}")]
		public async Task<IActionResult> Update(Guid userId, [FromBody] User user)
		{
			try
			{
				await _userService.UpdateAsync(userId, user);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPut("by-name/{userName}")]
		public async Task<IActionResult> UpdateByName(string userName, [FromBody] User user)
		{
			try
			{
				await _userService.UpdateAsync(userName, user);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{userId}")]
		public async Task<IActionResult> Delete(Guid userId)
		{
			try
			{
				await _userService.DeleteAsync(userId);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("by-name/{userName}")]
		public async Task<IActionResult> DeleteByName(string userName)
		{
			try
			{
				await _userService.DeleteAsync(userName);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("{userId}/activities")]
		public async Task<IActionResult> GetUserActivities(Guid userId)
		{
			var activities = await _userService.GetActivitiesAsync(userId);
			return Ok(activities);
		}

		[HttpGet("by-name/{userName}/activities")]
		public async Task<IActionResult> GetUserActivitiesByName(string userName)
		{
			var activities = await _userService.GetActivitiesByNameAsync(userName);
			return Ok(activities);
		}

		[HttpGet("{userId}/content")]
		public async Task<IActionResult> GetUserContent(Guid userId)
		{
			var content = await _userService.GetContentAsync(userId);
			return Ok(content);
		}

		[HttpGet("by-name/{userName}/content")]
		public async Task<IActionResult> GetUserContentByName(string userName)
		{
			var content = await _userService.GetContentByNameAsync(userName);
			return Ok(content);
		}

		[HttpGet("{userId}/plans")]
		public async Task<IActionResult> GetUserPlans(Guid userId)
		{
			var plans = await _userService.GetPlansAsync(userId);
			return Ok(plans);
		}

		[HttpGet("by-name/{userName}/plans")]
		public async Task<IActionResult> GetUserPlansByName(string userName)
		{
			var plans = await _userService.GetPlansByNameAsync(userName);
			return Ok(plans);
		}

		[HttpPost("{userId}/connect-plan/{planId}")]
		public async Task<IActionResult> ConnectUserPlan(Guid userId, Guid planId)
		{
			await _userService.ConnectUserPlanAsync(userId, planId);
			return Ok();
		}

		[HttpDelete("{userId}/disconnect-plan/{planId}")]
		public async Task<IActionResult> DisconnectUserPlan(Guid userId, Guid planId)
		{
			await _userService.DisconnectUserPlanAsync(userId, planId);
			return Ok();
		}

		[HttpPost("{userId}/connect-activity/{activityId}")]
		public async Task<IActionResult> ConnectUserActivity(Guid userId, Guid activityId)
		{
			await _userService.ConnectUserActivityAsync(userId, activityId);
			return Ok();
		}

		[HttpDelete("{userId}/disconnect-activity/{activityId}")]
		public async Task<IActionResult> DisconnectUserActivity(Guid userId, Guid activityId)
		{
			await _userService.DisconnectUserActivityAsync(userId, activityId);
			return Ok();
		}

		[HttpPost("{userId}/connect-content/{contentId}")]
		public async Task<IActionResult> ConnectUserContent(Guid userId, Guid contentId)
		{
			await _userService.ConnectUserContentAsync(userId, contentId);
			return Ok();
		}

		[HttpDelete("{userId}/disconnect-content/{contentId}")]
		public async Task<IActionResult> DisconnectUserContent(Guid userId, Guid contentId)
		{
			await _userService.DisconnectUserContentAsync(userId, contentId);
			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginUserAsync(string email, string password)
		{
			if(await _userService.LoginUserAsync(email, password))
				return Ok(_userService.GetByEmailAsync(email).Result.Id);
			return BadRequest();
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginUserAsync(Guid userId, string password)
		{
			if (await _userService.LoginUserAsync(userId, password))
				return Ok(userId);
			return BadRequest();
		}
	}
}