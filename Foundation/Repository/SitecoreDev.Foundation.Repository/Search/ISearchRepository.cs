using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sitecore.ContentSearch.SearchTypes;

namespace SitecoreDev.Foundation.Repository.Search
{
  public interface ISearchRepository
  {
    IEnumerable<T> Search<T>(Expression<Func<T, bool>> query) where T : SearchResultItem;
  }
}