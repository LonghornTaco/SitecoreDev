using SitecoreDev.Foundation.Ioc.Pipelines.InitializeContainer;
using SitecoreDev.Foundation.Repository.Content;

namespace SitecoreDev.Foundation.Repository.Pipelines.InitializeContainer
{
   public class RegisterDependencies
   {
      public void Process(InitializeContainerArgs args)
      {
         args.Container.Register<IContentRepository, SitecoreContentRepository>();
      }
   }
}
