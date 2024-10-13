using Advert.Entity.DTOs.AdvertsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.Services.Abstractions
{
    public interface IAdvertService
    {
        Task<AdvertListDto> GetAllByPagingAsync(Guid? categoryId, bool isAscending = false);
        Task CreateAdvertAsync(AdvertAddDto advertAddDto);
        Task<List<AdvertDto>> GetAllArticlesWithCategoryNonDeletedAsync();
        Task<AdvertDto> GetArticleWithCategoryNonDeletedAsync(Guid advertId);
        Task<string> UpdateArticleAsync(AdvertUpdateDto articleUpdateDto);
        Task<string> SafeDeleteArticleAsync(Guid articleId);
        Task<List<AdvertDto>> GetAllArticlesWithCategoryDeletedAsync();
        Task<string> UndoDeleteArticleAsync(Guid articleId);
        Task<AdvertListDto> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false);

    }
}
