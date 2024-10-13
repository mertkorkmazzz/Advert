using Advert.Dal.UnitOfWorks;
using Advert.Entity.DTOs.AdvertsDTOs;
using Advert.Entity.Entities;
using Advert.Services.Extensions;
using Advert.Services.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Advert.Services.Services.Concretae
{
    public class AdvertService : IAdvertService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _User;

        public AdvertService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            _User = httpContextAccessor.HttpContext.User;
        }




        // LİSTELEME 
        public async Task<AdvertListDto> GetAllByPagingAsync(Guid? categoryId, bool isAscending = false)
        {
            var adverts = categoryId == null
                ? await _unitOfWork.GetRepository<AdvertsEntity>().GetAllAsync(a => !a.IsDeleted, a => a.Category, u => u.User)
                : await _unitOfWork.GetRepository<AdvertsEntity>().GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted,
                    a => a.Category, u => u.User);

            var sortedAdverts = isAscending
                ? adverts.OrderBy(a => a.CreatedDate).ToList() // Artan sırayla listele
                : adverts.OrderByDescending(a => a.CreatedDate).ToList(); // Azalan sırayla listele

            return new AdvertListDto
            {
                Adverts = sortedAdverts,
                CategoryId = categoryId == null ? null : categoryId.Value,      
                TotalCount = adverts.Count,
                IsAscending = isAscending
            };
        }


        //EKLEME
        public async Task CreateAdvertAsync(AdvertAddDto advertAddDto)
        {
            // Kullanıcı bilgilerini alma
            var userId = _User.GetLoggedInUserId();
            var userEmail = _User.GetLoggedInEmail();

            // Yeni ilan oluşturma
            var advert = new AdvertsEntity(
                advertAddDto.Description,
                advertAddDto.Price,
                advertAddDto.Address,
                userId,           // Guid türünde UserId
                advertAddDto.CategoryId, // Guid türünde CategoryId
                userEmail         // string türünde CreatedBy (email)
            );

            // Veritabanına ekleme işlemi
            await _unitOfWork.GetRepository<AdvertsEntity>().AddAsync(advert);
            await _unitOfWork.SaveAsync();
        }


        //SİLİNMEMİŞ İLANLARI GETİRİR
        public async Task<List<AdvertDto>> GetAllArticlesWithCategoryNonDeletedAsync()
        {

            var articles = await _unitOfWork.GetRepository<AdvertsEntity>().GetAllAsync(x => !x.IsDeleted, x => x.Category);
            var map = _mapper.Map<List<AdvertDto>>(articles);

            return map;
        }


        public async Task<AdvertDto> GetArticleWithCategoryNonDeletedAsync(Guid advertId)
        {

            var article = await _unitOfWork.GetRepository<AdvertsEntity>().GetAsync(x => !x.IsDeleted && x.Id == advertId, x => x.Category, u => u.User);
            var map = _mapper.Map<AdvertDto>(article);

            return map;
        }



        public async Task<string> UpdateArticleAsync(AdvertUpdateDto articleUpdateDto)
        {
            var userEmail = _User.GetLoggedInEmail();
            var article = await _unitOfWork.GetRepository<AdvertsEntity>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category);


            _mapper.Map(articleUpdateDto, article);

            article.ModifiedDate = DateTime.Now;
            article.ModifiedBy = userEmail;

            await _unitOfWork.GetRepository<AdvertsEntity>().UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return article.Description;

        }


        public async Task<string> SafeDeleteArticleAsync(Guid articleId)
        {
            var userEmail = _User.GetLoggedInEmail();
            var article = await _unitOfWork.GetRepository<AdvertsEntity>().GetByGuidAsync(articleId);

            article.IsDeleted = true;
            article.DeletedDate = DateTime.Now;
            article.DeletedBy = userEmail;

            await _unitOfWork.GetRepository<AdvertsEntity>().UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return article.Description;
        }


        public async Task<List<AdvertDto>> GetAllArticlesWithCategoryDeletedAsync()
        {
            var articles = await _unitOfWork.GetRepository<AdvertsEntity>().GetAllAsync(x => x.IsDeleted, x => x.Category);
            var map = _mapper.Map<List<AdvertDto>>(articles);

            return map;
        }

        public async Task<string> UndoDeleteArticleAsync(Guid articleId)
        {
            var article = await _unitOfWork.GetRepository<AdvertsEntity>().GetByGuidAsync(articleId);

            article.IsDeleted = false;
            article.DeletedDate = null;
            article.DeletedBy = null;

            await _unitOfWork.GetRepository<AdvertsEntity>().UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return article.Description;
        }


        public async Task<AdvertListDto> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var articles = await _unitOfWork.GetRepository<AdvertsEntity>().GetAllAsync(
                a => !a.IsDeleted && (a.Description.Contains(keyword) || a.Address.Contains(keyword) || a.Category.Name.Contains(keyword)),
            a => a.Category, u => u.User);

            var sortedArticles = isAscending
                ? articles.OrderBy(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new AdvertListDto
            {
                Adverts = sortedArticles,
                TotalCount = articles.Count,
                IsAscending = isAscending
            };
        }
    }

}
