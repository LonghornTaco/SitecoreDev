using System.Collections.Generic;
using Sitecore.ContentSearch.SearchTypes;

namespace SitecoreDev.Feature.Search.Services
{
   public interface ISearchService
   {
      IEnumerable<SearchResultItem> SearchBlogPosts(string searchTerm);
   }
}