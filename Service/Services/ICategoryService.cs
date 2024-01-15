using Entity.DTOs.Categories;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public interface ICategoryService
	{
		Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
		Task CreateCategory(CategoryAddDto categoryAddDto);
		Task<Category> GetCategoryById(Guid id);
		Task<string> UpdateCategory(CategoryUpdateDto categoryUpdateDto);
		Task<string> DeleteCategory(Guid categoryId);
		Task<List<CategoryDto>> GetAllCategoriesDeleted();
		Task<string> UndoDeleteCategoryAsync(Guid categoryId);
		Task<List<CategoryDto>> GetAllCategoriesNonDeletedTake24();
    }
}
