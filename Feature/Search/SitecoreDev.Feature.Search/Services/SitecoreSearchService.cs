using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Sitecore.ContentSearch.SearchTypes;
using SitecoreDev.Foundation.Repository.Search;

namespace SitecoreDev.Feature.Search.Services
{
  public class SitecoreSearchService : ISearchService
  {
    private readonly ISearchRepository _searchRepository;

    public SitecoreSearchService(ISearchRepository searchRepository)
    {
      _searchRepository = searchRepository;
    }

    public IEnumerable<SearchResultItem> SearchBlogPosts(string searchTerm)
    {
      var results = _searchRepository.Search(q => q.Name.Contains(searchTerm));
      Debug.WriteLine("Count = " + results.Count());
      return results;
    }
  }
}