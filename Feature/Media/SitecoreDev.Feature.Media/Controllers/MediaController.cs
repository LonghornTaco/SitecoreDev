using System;
using System.Linq;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;
using SitecoreDev.Feature.Media.Models;
using SitecoreDev.Feature.Media.Services;
using SitecoreDev.Feature.Media.ViewModels;
using SitecoreDev.Foundation.Repository.Context;

namespace SitecoreDev.Feature.Media.Controllers
{
   public class MediaController : Controller
   {
      private readonly IContextWrapper _contextWrapper;
      private readonly IMediaContentService _mediaContentService;

      public MediaController(IContextWrapper contextWrapper, IMediaContentService mediaContentService)
      {
         _contextWrapper = contextWrapper;
         _mediaContentService = mediaContentService;
      }

      public ViewResult HeroSlider()
      {
         var viewModel = new HeroSliderViewModel();

         if (!String.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
         {
            var contentItem = _mediaContentService.GetHeroSliderContent(RenderingContext.Current.Rendering.DataSource);
            foreach (var slide in contentItem?.Slides)
            {
               viewModel.HeroImages.Add(new HeroSliderImageViewModel()
               {
                  Id = slide.Id.ToString(),
                  MediaUrl = slide.Image?.Src,
                  AltText = slide.Image?.Alt
               });
            }
            var firstItem = viewModel.HeroImages.FirstOrDefault();
            firstItem.IsActive = true;
            viewModel.ParentGuid = contentItem.Id.ToString();
         }

         var parameterValue = _contextWrapper.GetParameterValue("Slide Interval in Milliseconds");

         int interval = 0;
         if (int.TryParse(parameterValue, out interval))
            viewModel.SlideInterval = interval;

         viewModel.IsInExperienceEditorMode = _contextWrapper.IsExperienceEditor;

         return View(viewModel);
      }

      //public ViewResult OldHeroSlider()
      //{
      //   var viewModel = new HeroSliderViewModel();

      //   if (!String.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
      //   {
      //      var contentItem = _repository.GetItem(RenderingContext.Current.Rendering.DataSource);
      //      if (contentItem != null)
      //      {
      //         var heroImagesField = new MultilistField(contentItem.Fields["Hero Images"]);
      //         if (heroImagesField != null)
      //         {
      //            var items = heroImagesField.GetItems();
      //            var itemCounter = 0;
      //            foreach (var item in items)
      //            {
      //               var mediaItem = (MediaItem)item;
      //               viewModel.HeroImages.Add(new HeroSliderImageViewModel()
      //               {
      //                  MediaUrl = MediaManager.GetMediaUrl(mediaItem),
      //                  AltText = mediaItem.Alt,
      //                  IsActive = itemCounter == 0
      //               });
      //               itemCounter++;
      //            }
      //         }
      //      }
      //   }

      //   var parameterValue = _contextWrapper.GetParameterValue("Slide Interval in Milliseconds");

      //   int interval = 0;
      //   if (int.TryParse(parameterValue, out interval))
      //      viewModel.SlideInterval = interval;

      //   return View(viewModel);
      //}
   }
}