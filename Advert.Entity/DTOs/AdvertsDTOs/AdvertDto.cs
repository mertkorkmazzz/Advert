using Advert.Entity.DTOs.CategoryDTOs;
using Advert.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.DTOs.AdvertsDTOs
{
    public class AdvertDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public CategoryDto Category { get; set; }
        public AppUser User { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewCount { get; set; }
    }
}
