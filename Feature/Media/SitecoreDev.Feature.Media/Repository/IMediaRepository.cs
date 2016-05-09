using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace SitecoreDev.Feature.Media.Repository
{
   public interface IMediaRepository
   {
      Item GetItem(string contentGuid);
   }
}