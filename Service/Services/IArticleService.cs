using Entity.DTOs.Articles;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeleted();
        Task<ArticleDto> GetArticleWithCategoryNonDeleted(Guid articleId);
		Task CreateArticle(ArticleAddDto articleAddDto);
        Task<string> UpdateArticle(ArticleUpdateDto articleUpdateDto);
        Task<string> DeleteArticle(Guid articleId);
        Task<string> UndoDeleteArticleAsync(Guid articleId);
        Task<List<ArticleDto>> GetAllArticlesWithCategoryDeletedAsync();
        Task<ArticleListDto> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        Task<ArticleListDto> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        Task<List<ArticleDto>> GetMostReadArticlesAsync();
    }
}
