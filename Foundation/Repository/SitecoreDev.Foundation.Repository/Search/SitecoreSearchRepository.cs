using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace SitecoreDev.Foundation.Repository.Search
{
  public class SitecoreSearchRepository : ISearchRepository
  {
    public IEnumerable<SearchResultItem> Search(Expression<Func<SearchResultItem, bool>> query)
    {
      IEnumerable<SearchResultItem> results = null;

      var index = String.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);

      using (var context = ContentSearchManager.GetIndex(index).CreateSearchContext())
      {
        results = context.GetQueryable<SearchResultItem>().Where(query).ToList();
      }

      return results;
    }
  }
}