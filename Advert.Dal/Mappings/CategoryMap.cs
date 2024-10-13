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
    public class CategoryMap : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasData(new CategoryEntity
            {
                Id = Guid.Parse("A26D552E-C587-4DFB-973D-D24C608104DB"),
                Name = "Apartment",
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new CategoryEntity
            {
                Id = Guid.Parse("4EA08055-8957-4006-963E-23E13652FDDB"),
                Name = "House",
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            });
        }
    }
}
