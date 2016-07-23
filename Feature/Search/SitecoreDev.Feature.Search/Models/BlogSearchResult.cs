using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.SearchTypes;

namespace SitecoreDev.Feature.Search.Models
{
  public class BlogSearchResult : SearchResultItem
  {
    public string Title { get; set; }
  }
}