using System;
using System.Collections.Generic;
using System.Text;
using ThePongMobile.Services;
using TinyIoC;

namespace ThePongMobile.ViewModels
{
    public class IoContainer
    {
        /*
        public static NavigationService _navigationService;
        public static NetworkService _networkService;
        public IoContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<INavigationService, NavigationService>();
            container.RegisterType<INetworkService, NetworkService>();
            _navigationService = container.Resolve<NavigationService>();
            _networkService = container.Resolve<NetworkService>();
            
        }
        */
        private static TinyIoCContainer _container;

        static IoContainer()
        {
            _container = new TinyIoCContainer();
            _container.Register<LoginPageViewModel>();
            _container.Register<SetupPageViewModel>();
            _container.Register<MainPageViewModel>();

            _container.Register<INavigationService, NavigationService>();
            _container.Register<INetworkService, NetworkService>();
        }
        
    }
}
