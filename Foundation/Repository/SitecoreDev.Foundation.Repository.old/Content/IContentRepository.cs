using SitecoreDev.Foundation.Model;

namespace SitecoreDev.Foundation.Repository.Content
{
   public interface IContentRepository
   {
      T GetContentItem<T>(string contentGuid) where T : class, ICmsEntity;
   }
}
