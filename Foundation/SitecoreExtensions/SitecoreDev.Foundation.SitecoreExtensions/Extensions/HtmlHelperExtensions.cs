using System.Web;
using DynamicPlaceholders.Mvc.Extensions;
using Sitecore.Mvc.Helpers;

namespace SitecoreDev.Foundation.SitecoreExtensions.Extensions
{
   public static class HtmlHelperExtensions
   {
      public static HtmlString DynamicPlaceholder(this SitecoreHelper helper, string placeholderName)
      {
         return SitecoreHelperExtensions.DynamicPlaceholder(helper, placeholderName);
      }
   }
}