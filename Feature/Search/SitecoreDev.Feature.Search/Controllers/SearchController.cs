using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitecoreDev.Feature.Search.Services;

namespace SitecoreDev.Feature.Search.Controllers
{
  public class SearchController : Controller
  {
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
      _searchService = searchService;
    }

    public ViewResult BlogSearch()
    {
      var results = _searchService.SearchBlogPosts("My");
      return View(results);
    }
  }
}