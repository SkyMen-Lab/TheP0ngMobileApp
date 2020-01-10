using System;
using System.Collections.Generic;
using System.Text;
using ThePongMobile.Services;
using TinyIoC;

namespace ThePongMobile.ViewModels
{
    public static class IoContainer
    {
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

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
