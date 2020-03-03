using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThePongMobile.Models;
using ThePongMobile.Services;
using ThePongMobile.ViewModels;
using ThePongMobile.ViewModels.Base;
using ThePongMobile.Views;
using TinyIoC;
using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IViewLocatorService _viewLocatorService;
        private readonly IStorageService<SettingsModel> _storageService;
        public NavigationService(IViewLocatorService viewLocatorService, IStorageService<SettingsModel> storageService)
        {
            _viewLocatorService = viewLocatorService;
            _storageService = storageService;
        }
        
        public async Task PreviousPage() 
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task InitAsync()
        {
            if (!_storageService.IsSetup())
                await NavigateToAsync<SetupPageViewModel>();
            else
                await NavigateToAsync<LoginPageViewModel>();
        }

        public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            await NavigateToHelperAsync<TViewModel>(null);
        }

        public async Task NavigateToAsync<TViewModel>(object param) where TViewModel : BaseViewModel
        {
            await NavigateToHelperAsync<TViewModel>(param);
        }

        private async Task NavigateToHelperAsync<TViewModel>(object param) where TViewModel : BaseViewModel
        {
            var viewModel = IoContainer.Resolve(typeof(TViewModel)) as TViewModel;
            var page = _viewLocatorService.GetPageFromViewModel(viewModel);
            
            if (page is SetupPage) Application.Current.MainPage = new CustomNavPage(page);
            else
            {
                if (Application.Current.MainPage is CustomNavPage navPage) await navPage.PushAsync(page);
                else
                {
                    Application.Current.MainPage = new CustomNavPage(page);
                }
            }
        }
        

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBackStackAsync()
        {
            throw new NotImplementedException();
        }
        public string SendGameCode(string gameCode)
        {
            return gameCode;
        }
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }
    }
}
