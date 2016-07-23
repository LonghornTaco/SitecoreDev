using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
      var resultsViewModel = new SearchResultsViewModel();

      var results = _searchService.SearchBlogPosts(viewModel.SearchTerm, _contextWrapper.CurrentItemPath);

      foreach (var result in results)
      {
        resultsViewModel.Results.Add(new SearchResultViewModel()
        {
          Id = result.ItemId.ToString(),
          Title = result.Title
        });
      }

      return PartialView("~/Views/Search/_SearchResults.cshtml", resultsViewModel);
    }
  }
}