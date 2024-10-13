using Advert.Entity.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.DTOs.AdvertsDTOs
{
    public class AdvertAddDto
    {
        //ilan vermmek için gerekli dto lar 
        //açıklama  , fiyat , adress , ilanın catgorisi

        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public Guid CategoryId { get; set; }
        public IList<CategoryDto> Categories { get; set; }
    }
}
