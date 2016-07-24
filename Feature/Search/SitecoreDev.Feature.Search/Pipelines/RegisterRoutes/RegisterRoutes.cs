using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace SitecoreDev.Feature.Search.Pipelines.RegisterRoutes
{
  public class RegisterRoutes
  {
    public void Process(PipelineArgs args)
    {
      RouteTable.Routes.MapRoute("SubmitSearch", "Search/SubmitSearch", new { controller = "Search", action = "SubmitSearch" });
    }
  }
}