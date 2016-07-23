using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Sitecore.Pipelines;

namespace SitecoreDev.Feature.Search.Pipelines.RegisterRoutes
{
  //public class RegisterRoutes
  //{
  //  public void Process(PipelineArgs args)
  //  {
  //    //RouteTable.Routes.MapRoute("SubmitSearch", "Search/SubmitSearch", new { controller = "Search", action = "SubmitSearch" });
  //  }
  //}
  public class RegisterRoutes : Sitecore.Mvc.Pipelines.Loader.InitializeRoutes
  {
    public override void Process(PipelineArgs args)
    {
      Register(RouteTable.Routes);
    }

    protected virtual void Register(RouteCollection routes)
    {
      routes.MapRoute("SubmitSearch", "Search/SubmitSearch", new { controller = "Search", action = "SubmitSearch" });
    }
  }
}