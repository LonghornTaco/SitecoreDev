using System.Collections.Generic;
using SitecoreDev.Feature.Search.Models;
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

    public IEnumerable<BlogSearchResult> SearchBlogPosts(string searchTerm)
    {
      return _searchRepository.Search<BlogSearchResult>(q => q.Title.Contains(searchTerm) && q.Path.StartsWith("/sitecore/content/Home"));
    }
  }
}