using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace SitecoreDev.Feature.Media.Repository
{
   public class SitecoreMediaRepository : IMediaRepository
   {
      private Database _database;

      public SitecoreMediaRepository()
      {
         _database = Context.ContentDatabase ?? Context.Database;
      }

      public Item GetItem(string contentGuid)
      {
         return _database.GetItem(new ID(contentGuid));
      }
   }
}