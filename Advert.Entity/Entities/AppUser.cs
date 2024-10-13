using Advert.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.Entities
{
    public class AppUser : IdentityUser<Guid> ,IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<AdvertsEntity> advertsEntities { get; set; }
    }
}
