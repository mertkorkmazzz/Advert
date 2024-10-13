using Advert.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.Entities
{
    public class AdvertsEntity :EntityBase
    {
        public AdvertsEntity()
        {

        }

        public AdvertsEntity(string description, decimal price, string address, Guid userId, Guid categoryId, string createdBy)
        {
            Description = description;
            Price = price;
            Address = address;
            UserId = userId;
            CategoryId = categoryId;
            CreatedBy = createdBy;
        }


        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public int ViewCount { get; set; } = 0;

        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public ICollection<AdvertsVisitorEntity>  advertsVisitors { get; set; }


    }
}

