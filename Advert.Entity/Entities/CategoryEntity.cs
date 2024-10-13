using Advert.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.Entities
{
    public class CategoryEntity: EntityBase
    {


        public CategoryEntity()
        {

        }

        public CategoryEntity(string name, string createdBy)
        {
            Name = name;
            CreatedBy = createdBy;
        }




        public string Name { get; set; }
        public ICollection<AdvertsEntity> Listings { get; set; }
    }
}
