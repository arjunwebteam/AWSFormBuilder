using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class Flyers
    {
        public Int64 RId { get; set; }

        public Int64 FlyerId { get; set; }

        public string FlyerUrl { get; set; }

        public string RedirectUrl { get; set; }

        public Boolean IsActive { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string PageType { get; set; }

        public string PageContent { get; set; }
    }
}
