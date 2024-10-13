using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Advert.Entity.DTOs.AdvertsDTOs;
using Advert.Services.Services.Abstractions;
using Advert.Entity.Entities;
using AdvertBlog.Consts;
using AdvertBlog.ResultMessages;
using FluentValidation.AspNetCore;

namespace Advert.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertController : Controller
    {
        private readonly IAdvertService advertService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<AdvertsEntity> validator;
        private readonly IToastNotification toast;

        public AdvertController(IAdvertService advertService, ICategoryService categoryService, IMapper mapper, IValidator<AdvertsEntity> validator, IToastNotification toast)
        {
            this.advertService = advertService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toast = toast;
        }



        public IActionResult AdminesHomes()
        {
            return View();
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}, {RoleConsts.User}")]
        public async Task<IActionResult> AdvertisListis()
        {
            var advertsListDto = await advertService.GetAllByPagingAsync(null);
            return View(advertsListDto);
        }





        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> DeletedAdverts()
        {
            var deletedAdverts = await advertService.GetAllArticlesWithCategoryDeletedAsync();
            return View(deletedAdverts);
        }





        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(new AdvertAddDto { Categories = categories });
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Add(AdvertAddDto advertAddDto)
        {
            var advertEntity = mapper.Map<AdvertsEntity>(advertAddDto);
            var result = await validator.ValidateAsync(advertEntity);

            if (result.IsValid)
            {
                await advertService.CreateAdvertAsync(advertAddDto);
                toast.AddSuccessToastMessage(Messages.Advert.Add(advertAddDto.Description), new ToastrOptions { Title = "Success" });
                return RedirectToAction("AdminesHomes");
            }

            result.AddToModelState(this.ModelState);
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            advertAddDto.Categories = categories;
            return View(advertAddDto);
        }





        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}, {RoleConsts.User}")]
        public async Task<IActionResult> AdvertisListUpdate()
        {
            var advertsListDto = await advertService.GetAllByPagingAsync(null);
            return View(advertsListDto);
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid advertId)
        {
            var advert = await advertService.GetArticleWithCategoryNonDeletedAsync(advertId);
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            var advertUpdateDto = mapper.Map<AdvertUpdateDto>(advert);
            advertUpdateDto.Categories = categories;

            return View(advertUpdateDto);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Update(AdvertUpdateDto advertUpdateDto)
        {
            var advertEntity = mapper.Map<AdvertsEntity>(advertUpdateDto);
            var result = await validator.ValidateAsync(advertEntity);

            if (result.IsValid)
            {
                var title = await advertService.UpdateArticleAsync(advertUpdateDto);
                toast.AddSuccessToastMessage(Messages.Advert.Update(title), new ToastrOptions { Title = "Success" });
                return RedirectToAction("AdvertisListis");
            }

            result.AddToModelState(this.ModelState);
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            advertUpdateDto.Categories = categories;
            return View(advertUpdateDto);
        }




        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid advertId)
        {
            var title = await advertService.SafeDeleteArticleAsync(advertId);
            toast.AddSuccessToastMessage(Messages.Advert.Delete(title), new ToastrOptions { Title = "Success" });
            return RedirectToAction("Index");
        }




        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> UndoDelete(Guid advertId)
        {
            var title = await advertService.UndoDeleteArticleAsync(advertId);
            toast.AddSuccessToastMessage(Messages.Advert.UndoDelete(title), new ToastrOptions { Title = "Success" });
            return RedirectToAction("Index");
        }
    }
}
