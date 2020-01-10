using System;
using ThePongMobile.ViewModels;
using ThePongMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public class ViewLocatorService : IViewLocatorService
    {
        public Page GetPageFromViewModel<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            var page = (Page) Activator.CreateInstance(viewModel.PageType);
            page.BindingContext = viewModel;

            return page;
        }
    }
}