using Advert.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Entity.Entities
{
    public class AdvertsVisitorEntity : EntityBase
    {
        public AdvertsVisitorEntity() { }

        public AdvertsVisitorEntity(Guid articleId, int visitorId)
        {
            AdvertId = articleId;
            VisitorId = visitorId;
        }


        public Guid AdvertId { get; set; }
        public AdvertsEntity Article { get; set; }
        public int VisitorId { get; set; }
        public VisitorsEntity Visitor { get; set; }
    }
}
