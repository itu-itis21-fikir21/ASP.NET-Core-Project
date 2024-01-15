﻿using Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FluentValidations
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(c => c.Name)
				.NotEmpty()
				.NotNull()
				.MinimumLength(3)
				.MaximumLength(100)
				.WithName("Category name");
		}	
	}
}
