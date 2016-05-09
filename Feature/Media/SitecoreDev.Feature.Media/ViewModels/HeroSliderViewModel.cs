using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDev.Feature.Media.ViewModels
{
   public class HeroSliderViewModel
   {
      public List<HeroSliderImageViewModel> HeroImages { get; set; } = new List<HeroSliderImageViewModel>();
      public int ImageCount { get { return HeroImages.Count; } }
      public bool HasImages { get { return ImageCount > 0; } }
   }
}