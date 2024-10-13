using Advert.Entity.DTOs.CategoryDTOs;
using Advert.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
        Task<List<CategoryDto>> GetAllCategoriesNonDeletedTake24();
        Task<List<CategoryDto>> GetAllCategoriesDeleted();
        Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
        Task<CategoryEntity> GetCategoryByGuid(Guid id);
        Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<string> SafeDeleteCategoryAsync(Guid categoryId);
        Task<string> UndoDeleteCategoryAsync(Guid categoryId);
    }
}
