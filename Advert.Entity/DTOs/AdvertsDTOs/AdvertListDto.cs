using Advert.Entity.DTOs.CategoryDTOs;
using Advert.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.DTOs.AdvertsDTOs
{
    public class AdvertListDto
    {
        public Guid? CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual string  Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Address { get; set; }
       
        
        public virtual int TotalCount { get; set; }
        public virtual bool IsAscending { get; set; } = false;
        public Guid AdvertId { get; set; }
        public IList<AdvertsEntity> Adverts { get; set; }
    }
}
