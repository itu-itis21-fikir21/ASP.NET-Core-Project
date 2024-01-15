﻿using AutoMapper;
using DataAccess.UnitOfWorks;
using Entity.DTOs.Articles;
using Entity.DTOs.Categories;
using Entity.Entities;
using Microsoft.AspNetCore.Http;
using Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	internal class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ClaimsPrincipal _user;

		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.httpContextAccessor = httpContextAccessor;
			_user = httpContextAccessor.HttpContext.User;
		}
        public async Task<List<CategoryDto>> GetAllCategoriesNonDeleted()
		{
			var categories = await unitOfWork.GetRepository<Category>().GetAll(x => !x.IsDeleted);
			var map = mapper.Map<List<CategoryDto>>(categories);
			return map;
		}

		public async Task CreateCategory(CategoryAddDto categoryAddDto)
		{
			var userEmail = _user.GetLoggedInUserEmail();
			Category category = new(categoryAddDto.Name, userEmail);
			await unitOfWork.GetRepository<Category>().Add(category);
			await unitOfWork.SaveAsync();
			
		}

		public async Task<Category> GetCategoryById(Guid id)
		{
			var category = await unitOfWork.GetRepository<Category>().GetById(id);
			return category;
		}
		public async Task<string> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
		{
			var userEmail = _user.GetLoggedInUserEmail();

			var category = await unitOfWork.GetRepository<Category>().GetAsync(x => !x.IsDeleted && x.Id == categoryUpdateDto.Id);
			category.Name = categoryUpdateDto.Name;
			

			await unitOfWork.GetRepository<Category>().Update(category);
			await unitOfWork.SaveAsync();

			return category.Name;
		}
		public async Task<string> DeleteCategory(Guid categoryId)
		{
			var userEmail= _user.GetLoggedInUserEmail();	
			var category = await unitOfWork.GetRepository<Category>().GetById(categoryId);

			category.IsDeleted = true;
			category.DeletedDate = DateTime.Now;
			category.DeletedBy = userEmail;
			await unitOfWork.GetRepository<Category>().Update(category);
			await unitOfWork.SaveAsync();

			return category.Name;

		}
        public async Task<List<CategoryDto>> GetAllCategoriesDeleted()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAll(x => x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);
            return map;
        }
        public async Task<string> UndoDeleteCategoryAsync(Guid categoryId)
        {
            var category = await unitOfWork.GetRepository<Category>().GetById(categoryId);

            category.IsDeleted = false;
            category.DeletedDate = null;
            category.DeletedBy = null;

            await unitOfWork.GetRepository<Category>().Update(category);
            await unitOfWork.SaveAsync();

            return category.Name;
        }
        public async Task<List<CategoryDto>> GetAllCategoriesNonDeletedTake24()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAll(x => !x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);

            return map.Take(24).ToList();
        }
    }
}
