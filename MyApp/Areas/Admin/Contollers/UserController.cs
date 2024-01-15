﻿using AutoMapper;
using Entity.DTOs;
using Entity.DTOs.Users;
using Entity.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MyApp.ResultMessages;
using NToastNotify;
using Service.Extensions;
using Service.Services;

namespace MyApp.Areas.Admin.Contollers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly IUserService userService;
		private readonly IValidator<AppUser> validator;
		private readonly IToastNotification toast;
		private readonly IMapper mapper;

		public UserController(IUserService userService, IValidator<AppUser> validator, IToastNotification toast, IMapper mapper)
		{
			this.userService = userService;
			this.validator = validator;
			this.toast = toast;
			this.mapper = mapper;
		}
		public async Task<IActionResult> Index()
		{
			var result = await userService.GetAllUsersWithRoleAsync();

			return View(result);
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var roles = await userService.GetAllRolesAsync();
			return View(new UserAddDto { Roles = roles });
		}
		[HttpPost]
		public async Task<IActionResult> Add(UserAddDto userAddDto)
		{
			var map = mapper.Map<AppUser>(userAddDto);
			var validation = await validator.ValidateAsync(map);
			var roles = await userService.GetAllRolesAsync();

			if (ModelState.IsValid)
			{
				var result = await userService.CreateUserAsync(userAddDto);
				if (result.Succeeded)
				{
					toast.AddSuccessToastMessage(Messages.User.Add(userAddDto.Email), new ToastrOptions { Title = "Successful!" });
					return RedirectToAction("Index", "User", new { Area = "Admin" });
				}
				else
				{
					result.AddToIdentityModelState(this.ModelState);
					validation.AddToModelState(this.ModelState);
					return View(new UserAddDto { Roles = roles });

				}
			}
			return View(new UserAddDto { Roles = roles });
		}
		[HttpGet]
		public async Task<IActionResult> Update(Guid userId)
		{
			var user = await userService.GetAppUserByIdAsync(userId);

			var roles = await userService.GetAllRolesAsync();

			var map = mapper.Map<UserUpdateDto>(user);
			map.Roles = roles;
			return View(map);
		}
		[HttpPost]
		public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
		{
			var user = await userService.GetAppUserByIdAsync(userUpdateDto.Id);

			if (user != null)
			{
				var roles = await userService.GetAllRolesAsync();
				if (ModelState.IsValid)
				{
					var map = mapper.Map(userUpdateDto, user);
					var validation = await validator.ValidateAsync(map);

					if (validation.IsValid)
					{
						user.UserName = userUpdateDto.Email;
						user.SecurityStamp = Guid.NewGuid().ToString();
						var result = await userService.UpdateUserAsync(userUpdateDto);
						if (result.Succeeded)
						{
							toast.AddSuccessToastMessage(Messages.User.Update(userUpdateDto.Email), new ToastrOptions { Title = "Successful!" });
							return RedirectToAction("Index", "User", new { Area = "Admin" });
						}
						else
						{
							result.AddToIdentityModelState(this.ModelState);
							return View(new UserUpdateDto { Roles = roles });
						}
					}
					else
					{
						validation.AddToModelState(this.ModelState);
						return View(new UserUpdateDto { Roles = roles });
					}
				}
			}
			return NotFound();
		}
		public async Task<IActionResult> Delete(Guid userId)
		{
			var result = await userService.DeleteUserAsync(userId);

			if (result.identityResult.Succeeded)
			{
				toast.AddSuccessToastMessage(Messages.User.Delete(result.email), new ToastrOptions { Title = "Successful!" });
				return RedirectToAction("Index", "User", new { Area = "Admin" });
			}
			else
			{
				result.identityResult.AddToIdentityModelState(this.ModelState);
			}
			return NotFound();
		}
		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			var profile = await userService.GetUserProfileAsync();

			return View(profile);
		}
		[HttpPost]
		public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
		{

			if (ModelState.IsValid)
			{
				var result = await userService.UserProfileUpdateAsync(userProfileDto);
				if (result)
				{
					toast.AddSuccessToastMessage("Profile update completed", new ToastrOptions { Title = "Successful!" });
					return RedirectToAction("Index", "Home", new { Area = "Admin" });
				}
				else
				{
					var profile = await userService.GetUserProfileAsync();
					toast.AddErrorToastMessage("Profile update could not be completed", new ToastrOptions { Title = "Not Successful" });
					return View(profile);
				}
			}
			else
				return NotFound();
		}
	}
}
