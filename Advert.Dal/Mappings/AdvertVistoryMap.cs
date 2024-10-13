using Advert.Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Dal.Mappings
{
    public class AdvertVisitorMap : IEntityTypeConfiguration<AdvertsVisitorEntity>
    {
        public void Configure(EntityTypeBuilder<AdvertsVisitorEntity> builder)
        {
            builder.HasKey(x => new { x.AdvertId, x.VisitorId });
        }
    }
}
