using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using ThePongMobile.Models;
using ThePongMobile.Services;
using ThePongMobile.Views;
using Xamarin.Forms;
using System.Reflection;
using ThePongMobile.ViewModels.Base;

namespace ThePongMobile.ViewModels
{
    public class SetupPageViewModel : BaseViewModel
    {

        private bool _isEntryCodeCompleted = false;
        private bool _hasError;
        private string _schoolCode;
        private SchoolData _schoolData;
        private INavigationService _navigationService;
        private INetworkService _networkService;
        private IStorageService<ConnectionConfig> _storageService;
        
        public override Type PageType => typeof(SetupPage);
        public ICommand EntryCompletedCommand { get; private set; }
        public ICommand ContinueCommand { get; private set; }
        

        public bool HasError 
        {
            get => _hasError; 
            set => SetValue(ref _hasError, value);  
        }

        public string SchoolCode
        {
            get => _schoolCode;
            set => SetValue(ref _schoolCode, value);
        }
        
        public SetupPageViewModel(INavigationService navigationService, INetworkService networkService,
            IStorageService<ConnectionConfig> storageService)
        {
            _navigationService = navigationService;
            _networkService = networkService;
            _storageService = storageService;
            EntryCompletedCommand = new Command(EntryCodeCompleted);
            ContinueCommand = new Command(ContinueButtonPressed);
        }

        private async void ContinueButtonPressed()
        {
            if (_isEntryCodeCompleted)
            {
                _storageService.SetConfiguration(_schoolData.Config);
                await _navigationService.NavigateToAsync<LoginPageViewModel>();
            }
            else if (!_isEntryCodeCompleted)
                HasError = true;
        }

        private async void EntryCodeCompleted()
        {
            _schoolData = await _networkService.GetSchoolDataAsync(SchoolCode);
            
            if (_schoolData.Code == SchoolCode)
            {
                _isEntryCodeCompleted = true;
                HasError = false;
            }
            else
              HasError = true;
        }
    }
}