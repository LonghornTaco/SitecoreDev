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
    public IEnumerable<T> Search<T>(Expression<Func<T, bool>> query) where T : SearchResultItem
    {
      IEnumerable<T> results = null;

      var index = String.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);

      using (var context = ContentSearchManager.GetIndex(index).CreateSearchContext())
      {
        results = context.GetQueryable<T>().Where(query).ToList();
      }

      return results;
    }

    public IEnumerable<SearchIndexTerm> GetTermsByFieldName(string fieldName, string searchTerm)
    {
      IEnumerable<SearchIndexTerm> results = null;

      var index = String.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);

      using (var context = ContentSearchManager.GetIndex(index).CreateSearchContext())
      {
        results = context.GetTermsByFieldName(fieldName, searchTerm).ToList();
      }

      return results;
    }
  }
}