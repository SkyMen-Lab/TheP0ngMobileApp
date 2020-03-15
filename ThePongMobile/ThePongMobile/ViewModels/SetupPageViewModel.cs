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
using Xamarin.Essentials;
using ThePongMobile.ViewModels.Base;
using System.Threading.Tasks;

namespace ThePongMobile.ViewModels
{
    public class SetupPageViewModel : BaseViewModel
    {

        private bool _hasError;
        private string _schoolCode;
        private INavigationService _navigationService;
        private INetworkService _networkService;
        private IStorageService<SettingsModel> _storageService;
        
        public override Type PageType => typeof(SetupPage);
        public ICommand ContinueCommand { get; private set; }
        public ICommand TeamCodeHelpCommand { get; private set; }
        public ICommand AboutPageCommand { get; private set; }

        public bool HasError 
        {
            get => _hasError; 
            set => SetValue(ref _hasError, value);  
        }

        public string SchoolCode
        {
            get => _schoolCode;
            set => SetValue(ref _schoolCode, value?.ToUpper());
        }
        
        public SetupPageViewModel(INavigationService navigationService, INetworkService networkService, IStorageService<SettingsModel> storageService)
        {
            _navigationService = navigationService;
            _networkService = networkService;
            _storageService = storageService;
            ContinueCommand = new Command(ContinueButtonPressed);
            TeamCodeHelpCommand = new Command(TeamCodeHelp);
            AboutPageCommand = new Command(AboutPagePressed);
        }
        private async void AboutPagePressed()
        {
           await _navigationService.PushModalAsync<AboutPageViewModel>();
        }
        private async void TeamCodeHelp()
        {
             await _navigationService.DisplayInformation("Where to Find Team Code?"
                 , "If you are unsure about what the team code is or where to find it" +
                 ", you should ask a member of our team to find out more."
                 , "OK");
        }
        private async void ContinueButtonPressed()
        {
            await _navigationService.NavigateToAsync<LoginPageViewModel>();

            //if(_schoolCode == null)
            //{
            //    HasError = true;
            //    return;
            //}
            //var schoolData = await _networkService.GetSchoolDataAsync(_schoolCode);
            //if (schoolData != null && schoolData.Config != null && SchoolCode != null)
            //{
            //    var settings = new SettingsModel()
            //    {
            //        SchoolName = schoolData.Name,
            //        SchoolCode = SchoolCode,
            //        ConnectionType = schoolData.Config.ConnectionType,
            //        IP = schoolData.Config.IP,
            //        Port = schoolData.Config.Port
            //    };
            //    _storageService.SetConfiguration(settings);
            //    await _navigationService.NavigateToAsync<LoginPageViewModel>();
            //}
            //else
            //    HasError = true;

        }
    }
}