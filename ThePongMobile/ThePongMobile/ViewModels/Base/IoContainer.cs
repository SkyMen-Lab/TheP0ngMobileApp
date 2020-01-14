using System;
using ThePongMobile.Services;
using ThePongMobile.Services.Mocks;
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
            _container.Register(typeof(IStorageService<>), typeof(StorageService));
            if (useMocks)
            {
                _container.Register<INetworkService, NetworkServiceMock>();
            }
            else
            {
                throw new NotImplementedException();
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
