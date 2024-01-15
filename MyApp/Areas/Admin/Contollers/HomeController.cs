﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Services;

namespace MyApp.Areas.Admin.Contollers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;
		private readonly IDashbordService dashbordService;


		public HomeController(IArticleService articleService, IDashbordService dashbordService)
        {
            this.articleService = articleService;
			this.dashbordService = dashbordService;	
        }
        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryNonDeleted();
            return View(articles);
        }
		[HttpGet]
		public async Task<IActionResult> YearlyArticleCounts()
		{
			var count = await dashbordService.GetYearlyArticleCounts();
			return Json(JsonConvert.SerializeObject(count));
		}
		[HttpGet]
		public async Task<IActionResult> TotalArticleCount()
		{
			var count = await dashbordService.GetTotalArticleCount();
			return Json(count);
		}
		[HttpGet]
		public async Task<IActionResult> TotalCategoryCount()
		{
			var count = await dashbordService.GetTotalCategoryCount();
			return Json(count);
		}
        [HttpGet]
        public async Task<IActionResult> TotalUserCount()
        {
			var count = await dashbordService.GetTotalUserCount();
            return Json(count);
        }
    }
}
