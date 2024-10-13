using Advert.Dal.UnitOfWorks;
using Advert.Entity.DTOs.CategoryDTOs;
using Advert.Entity.Entities;
using Advert.Services.Extensions;
using Advert.Services.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.Services.Concretae
{
    public class CategoryService : ICategoryService
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

            var categories = await unitOfWork.GetRepository<CategoryEntity>().GetAllAsync(x => !x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);

            return map;
        }
        public async Task<List<CategoryDto>> GetAllCategoriesNonDeletedTake24()
        {
            var categories = await unitOfWork.GetRepository<CategoryEntity>().GetAllAsync(x => !x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);

            return map.Take(24).ToList();
        }

        public async Task CreateCategoryAsync(CategoryAddDto categoryAddDto)
        {
            var userEmail = _user.GetLoggedInEmail();

            CategoryEntity category = new(categoryAddDto.Name, userEmail);
            await unitOfWork.GetRepository<CategoryEntity>().AddAsync(category);
            await unitOfWork.SaveAsync();

        }
        public async Task<CategoryEntity> GetCategoryByGuid(Guid id)
        {
            var category = await unitOfWork.GetRepository<CategoryEntity>().GetByGuidAsync(id);
            return category;
        }
        public async Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var category = await unitOfWork.GetRepository<CategoryEntity>().GetAsync(x => !x.IsDeleted && x.Id == categoryUpdateDto.Id);

            category.Name = categoryUpdateDto.Name;
            category.ModifiedBy = userEmail;
            category.ModifiedDate = DateTime.Now;


            await unitOfWork.GetRepository<CategoryEntity>().UpdateAsync(category);
            await unitOfWork.SaveAsync();


            return category.Name;
        }

        public async Task<string> SafeDeleteCategoryAsync(Guid categoryId)
        {
            var userEmail = _user.GetLoggedInEmail();
            var category = await unitOfWork.GetRepository<CategoryEntity>().GetByGuidAsync(categoryId);

            category.IsDeleted = true;
            category.DeletedDate = DateTime.Now;
            category.DeletedBy = userEmail;

            await unitOfWork.GetRepository<CategoryEntity>().UpdateAsync(category);
            await unitOfWork.SaveAsync();

            return category.Name;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesDeleted()
        {
            var categories = await unitOfWork.GetRepository<CategoryEntity>().GetAllAsync(x => x.IsDeleted);
            var map = mapper.Map<List<CategoryDto>>(categories);

            return map;
        }

        public async Task<string> UndoDeleteCategoryAsync(Guid categoryId)
        {
            var category = await unitOfWork.GetRepository<CategoryEntity>().GetByGuidAsync(categoryId);

            category.IsDeleted = false;
            category.DeletedDate = null;
            category.DeletedBy = null;

            await unitOfWork.GetRepository<CategoryEntity>().UpdateAsync(category);
            await unitOfWork.SaveAsync();

            return category.Name;
        }
    }
}
