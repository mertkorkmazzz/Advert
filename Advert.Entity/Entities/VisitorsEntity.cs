using Advert.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.Entities
{
    public class VisitorsEntity : EntityBase
    {
        public VisitorsEntity() { }

        public VisitorsEntity(string ipAddress, string userAgent)
        {
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }



        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<AdvertsVisitorEntity> ArticleVisitors { get; set; }
    }
}
