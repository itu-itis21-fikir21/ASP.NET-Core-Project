using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace MyApp.ViewComponents
{
    public class HomeArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;

        public HomeArticlesViewComponent(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await articleService.GetMostReadArticlesAsync();
            return View(articles);  
        }
    }
}
