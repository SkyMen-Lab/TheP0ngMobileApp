using System;
using System.Collections.Generic;
using System.Text;
using ThePongMobile.Services;
using Unity;

namespace ThePongMobile.ViewModels
{
    public class Containers
    {
        public static NavigationService _navigationService;
        public static NetworkService _networkService;
        public Containers()
        {
            var container = new UnityContainer();
            container.RegisterType<INavigationService, NavigationService>();
            container.RegisterType<INetworkService, NetworkService>();
            _navigationService = container.Resolve<NavigationService>();
            _networkService = container.Resolve<NetworkService>();
        }
    }
}
