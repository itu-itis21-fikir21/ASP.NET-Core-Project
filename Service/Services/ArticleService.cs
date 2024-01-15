using AutoMapper;
using DataAccess.UnitOfWorks;
using Entity.DTOs.Articles;
using Entity.Entities;
using Entity.Enums;
using Microsoft.AspNetCore.Http;
using Service.Extensions;
using Service.Helpers.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IImageHelper imageHelper;
		private readonly ClaimsPrincipal _userId;
		public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IImageHelper imageHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
			this.httpContextAccessor = httpContextAccessor;
			this.imageHelper = imageHelper;
			_userId = httpContextAccessor.HttpContext.User;
		}

		public async Task CreateArticle(ArticleAddDto articleAddDto)
		{
            var userId = _userId.GetLoggedInUserId();
            var userEmail = _userId.GetLoggedInUserEmail();
            var imageUpload = await imageHelper.Upload(articleAddDto.Title, articleAddDto.Photo, ImageType.Post);
            Image image = new(imageUpload.FullName, articleAddDto.Photo.ContentType, userEmail);
			await unitOfWork.GetRepository<Image>().Add(image);

			var article = new Article(articleAddDto.Title, articleAddDto.Content, userId, userEmail, articleAddDto.CategoryId, image.Id);
           
            await unitOfWork.GetRepository<Article>().Add(article);
            await unitOfWork.SaveAsync();
		}

		public async Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeleted()
        {
            var articles = await unitOfWork.GetRepository<Article>().GetAll(x=>!x.IsDeleted, x=>x.Category);
            var map = mapper.Map<List<ArticleDto>>(articles);
            return map;
        }
		public async Task<ArticleDto> GetArticleWithCategoryNonDeleted(Guid articleId)
		{
			var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category, i=>i.Image);
			var map = mapper.Map<ArticleDto>(article);
			return map;
		}

        public async Task<string> UpdateArticle(ArticleUpdateDto articleUpdateDto)
        {
			var userEmail = _userId.GetLoggedInUserEmail();

			var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category, i=>i.Image);
            if (articleUpdateDto.Photo != null)
            {
                imageHelper.Delete(article.Image.FileName);
                var imageUpload = await imageHelper.Upload(articleUpdateDto.Title, articleUpdateDto.Photo, ImageType.Post);
                Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType, userEmail);
                await unitOfWork.GetRepository<Image>().Add(image);
                article.ImageId = image.Id;
            }
            article.Title = articleUpdateDto.Title;
            article.Content = articleUpdateDto.Content;
            article.CategoryId = articleUpdateDto.CategoryId;

            await unitOfWork.GetRepository<Article>().Update(article);
            await unitOfWork.SaveAsync();
            return article.Title;
		}
        public async Task<string> DeleteArticle(Guid articleId)
        {
			var userEmail = _userId.GetLoggedInUserEmail();

			var article = await unitOfWork.GetRepository<Article>().GetById(articleId);

            article.IsDeleted = true;  
            article.DeletedDate = DateTime.Now;
            article.DeletedBy = userEmail;
            await unitOfWork.GetRepository<Article>().Update(article);

            await unitOfWork.SaveAsync();
            return article.Title;
        }
        public async Task<string> UndoDeleteArticleAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetById(articleId);

            article.IsDeleted = false;
            article.DeletedDate = null;
            article.DeletedBy = null;

            await unitOfWork.GetRepository<Article>().Update(article);
            await unitOfWork.SaveAsync();

            return article.Title;
        }
        public async Task<List<ArticleDto>> GetAllArticlesWithCategoryDeletedAsync()
        {
            var articles = await unitOfWork.GetRepository<Article>().GetAll(x => x.IsDeleted, x => x.Category);
            var map = mapper.Map<List<ArticleDto>>(articles);

            return map;
        }
        public async Task<ArticleListDto> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var articles = categoryId == null
                ? await unitOfWork.GetRepository<Article>().GetAll(a => !a.IsDeleted, a => a.Category, i => i.Image, u => u.User)
                : await unitOfWork.GetRepository<Article>().GetAll(a => a.CategoryId == categoryId && !a.IsDeleted,
                    a => a.Category, i => i.Image, u => u.User);
            var sortedArticles = isAscending
                ? articles.OrderBy(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new ArticleListDto
            {
                Articles = sortedArticles,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                IsAscending = isAscending
            };
        }
        public async Task<ArticleListDto> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var articles = await unitOfWork.GetRepository<Article>().GetAll(
                a => !a.IsDeleted && (a.Title.Contains(keyword) || a.Content.Contains(keyword) || a.Category.Name.Contains(keyword)),
            a => a.Category, i => i.Image, u => u.User);

            var sortedArticles = isAscending
                ? articles.OrderBy(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new ArticleListDto
            {
                Articles = sortedArticles,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                IsAscending = isAscending
            };
        }

        public async Task<List<ArticleDto>> GetMostReadArticlesAsync()
        {
            var articles = await unitOfWork.GetRepository<Article>().GetAll(a => !a.IsDeleted);
            var sortedArticles = articles.OrderByDescending(x => x.ViewCount);
            var map = mapper.Map<List<ArticleDto>>(sortedArticles);
            return map.Take(3).ToList();
        }

        }


}
