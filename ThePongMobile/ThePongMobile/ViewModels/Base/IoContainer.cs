using System;
using ThePongMobile.Services;
using TinyIoC;
using Xamarin.Forms;

namespace ThePongMobile.ViewModels.Base
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
        }

        public static void RegisterServices(bool useMocks)
        {
            _container.Register<IViewLocatorService, ViewLocatorService>();
            _container.Register<INavigationService, NavigationService>();
            if (useMocks)
            {
                _container.Register<INetworkService, NetworkServiceMock>();
            }
            else
            {
                _container.Register<INetworkService, NetworkService>();
            }
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public static object Resolve<T>(T objType) where T: Type
        {
            return _container.Resolve(objType);
        }
    }
}
