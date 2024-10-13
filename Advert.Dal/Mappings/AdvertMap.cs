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
    
        public class AdvertMap : IEntityTypeConfiguration<AdvertsEntity>
        {
            public void Configure(EntityTypeBuilder<AdvertsEntity> builder)
            {
                builder.HasData(new AdvertsEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Luxury Apartment in City Center",
                    Price = 1200000,
                    Address = "123 Main St, City Center",
                    ViewCount = 10,
                    UserId = Guid.Parse("CB94223B-CCB8-4F2F-93D7-0DF96A7F065C"),
                    CategoryId = Guid.Parse("A26D552E-C587-4DFB-973D-D24C608104DB"),
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
               

                },
                new AdvertsEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Cozy House Near the Beach",
                    Price = 850000,
                    Address = "456 Ocean Blvd, Beachside",
                    ViewCount = 5,
                    UserId = Guid.Parse("3AA42229-1C0F-4630-8C1A-DB879ECD0427"),
                    CategoryId = Guid.Parse("4EA08055-8957-4006-963E-23E13652FDDB"),
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });
            }
        }
    }

