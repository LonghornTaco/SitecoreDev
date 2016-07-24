using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SitecoreDev.Feature.Search.Services;
using SitecoreDev.Feature.Search.ViewModels;
using SitecoreDev.Foundation.Repository.Context;

namespace SitecoreDev.Feature.Search.Controllers
{
  public class SearchController : Controller
  {
    private readonly ISearchService _searchService;
    private readonly IContextWrapper _contextWrapper;

    public SearchController(ISearchService searchService, IContextWrapper contextWrapper)
    {
      _searchService = searchService;
      _contextWrapper = contextWrapper;
    }

    public ViewResult BlogSearch()
    {
      return View(new BlogSearchViewModel());
    }

    [HttpPost]
    public PartialViewResult SubmitSearch(BlogSearchViewModel viewModel)
    {
      Func<IEnumerable<string>, string> stringify = (list) =>
      {
        StringBuilder sb = new StringBuilder();
        foreach (var item in list)
          sb.Append(String.Format("{0}; ", item));
        return sb.ToString();
      };

      var resultsViewModel = new SearchResultsViewModel();

      var results = _searchService.SearchBlogPosts(viewModel.SearchTerm);

      foreach (var result in results)
      {
        resultsViewModel.Results.Add(new SearchResultViewModel()
        {
          Id = result.ItemId.ToString(),
          Title = result.Title,
          Url = result.Url,
          Body = result.Body,
          CategoryList = stringify(result.Categories)
        });
      }

      return PartialView("~/Views/Search/_SearchResults.cshtml", resultsViewModel);
    }

    [HttpPost]
    public IEnumerable<string> GetSuggestions(BlogSearchViewModel viewModel)
    {
      var suggestions = new List<string>();

      if (!String.IsNullOrEmpty(viewModel?.SearchTerm))
      {
        return _searchService.GetSearchSuggestions(viewModel.SearchTerm);
      }

      return Enumerable.Empty<string>();
    }
  }
}