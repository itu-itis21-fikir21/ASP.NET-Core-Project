using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Extensions
{
	public static class FluentValidationExtensions
	{
		public static void addToModelState(this ValidationResult result, ModelStateDictionary modelState)
		{
			foreach(var error in result.Errors)
			{
				modelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
		}
		public static void AddToIdentityModelState(this IdentityResult result, ModelStateDictionary modelState)
		{
			foreach (var error in result.Errors)
			{
				modelState.AddModelError("", error.Description);
			}
		}
	}
}
