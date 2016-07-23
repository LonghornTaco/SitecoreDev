using System.Collections.Specialized;

namespace SitecoreDev.Foundation.Repository.Context
{
  public interface IContextWrapper
  {
    bool IsExperienceEditor { get; }
    string CurrentItemPath { get; }
    string GetParameterValue(string key);
  }
}