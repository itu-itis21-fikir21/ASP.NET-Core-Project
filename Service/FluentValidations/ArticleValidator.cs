﻿using Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FluentValidations
{
	public class ArticleValidator : AbstractValidator<Article>
	{
		public ArticleValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MinimumLength(3)
				.MaximumLength(150)
				.WithName("Title");
			RuleFor(x=> x.Content)
				.NotEmpty()
				.NotNull()
				.MinimumLength(3)
				.MaximumLength(1500)
				.WithName("Content");
		}
	}
}
