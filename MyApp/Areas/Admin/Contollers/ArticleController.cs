using AutoMapper;
using Entity.DTOs.Articles;
using Entity.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Consts;
using MyApp.ResultMessages;
using NToastNotify;
using Service.Services;
using System.Data;

namespace MyApp.Areas.Admin.Contollers
{
	[Area("Admin")]
	public class ArticleController : Controller
	{
		private readonly IArticleService articleService;
		private readonly ICategoryService categoryService;
		private readonly IMapper mapper;
		private readonly IValidator<Article> validator;
		private readonly IToastNotification toast;

		public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator, IToastNotification toast)
		{
			this.articleService = articleService;
			this.categoryService = categoryService;
			this.mapper = mapper;
			this.validator = validator;
			this.toast = toast;
		}

		[Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}, {RoleConsts.User}")]
		public async Task<IActionResult> Index()
		{

			var articles = await articleService.GetAllArticlesWithCategoryNonDeleted();
			return View(articles);
		}

		[HttpGet]
		[Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
		public async Task<IActionResult> Add()
		{
			var categories = await categoryService.GetAllCategoriesNonDeleted();
			return View(new ArticleAddDto { Categories = categories });
		}
		[HttpPost]
		[Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
		public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
		{
			var map = mapper.Map<Article>(articleAddDto);
			var result = await validator.ValidateAsync(map);
			if (result.IsValid)
			{
				await articleService.CreateArticle(articleAddDto);
				toast.AddSuccessToastMessage(Messages.Article.Add(articleAddDto.Title), new ToastrOptions { Title = "Successful!" });
				return RedirectToAction("Index", "Article", new { Area = "Admin" });
			}
			else
			{
				result.AddToModelState(this.ModelState);
				var categories = await categoryService.GetAllCategoriesNonDeleted();
				return View(new ArticleAddDto { Categories = categories });
			}


		}

		[HttpGet]
		[Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
		public async Task<IActionResult> Update(Guid ArticleId)
		{
			var article = await articleService.GetArticleWithCategoryNonDeleted(ArticleId);
			var categories = await categoryService.GetAllCategoriesNonDeleted();

			var articleUpdateDto = mapper.Map<ArticleUpdateDto>(article);
			articleUpdateDto.Categories = categories;

			return View(articleUpdateDto);


		}
		[HttpPost]
		[Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
		public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
		{
			var map = mapper.Map<Article>(articleUpdateDto);
			var result = await validator.ValidateAsync(map);
			if (result.IsValid)
			{
				var title = await articleService.UpdateArticle(articleUpdateDto);
				toast.AddSuccessToastMessage(Messages.Article.Update(title), new ToastrOptions { Title = "Successful!" });
				return RedirectToAction("Index", "Article", new { Area = "Admin" });
			}
			else
			{
				result.AddToModelState(this.ModelState);
			}
			var categories = await categoryService.GetAllCategoriesNonDeleted();
			articleUpdateDto.Categories = categories;

			return View(articleUpdateDto);


		}

		[Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
		public async Task<IActionResult> Delete(Guid articleId)
		{
			var title = await articleService.DeleteArticle(articleId);
			toast.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions { Title = "Successful!" });

			return RedirectToAction("Index", "Article", new { Area = "Admin" });
		}

		[Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
		public async Task<IActionResult> UndoDelete(Guid articleId)
        {
            var title = await articleService.UndoDeleteArticleAsync(articleId);
            toast.AddSuccessToastMessage(Messages.Article.UndoDelete(title), new ToastrOptions() { Title = "Successful!" });


            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
        public async Task<IActionResult> DeletedArticle()
        {
            var articles = await articleService.GetAllArticlesWithCategoryDeletedAsync();
            return View(articles);
        }
    }
}
