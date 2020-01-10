using System.Threading.Tasks;
using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public interface INavigationService
    {
        //TODO: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation 
        Task InitAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModels.BaseViewModel;
        Task NavigateToAsync<TViewModel>(object param) where TViewModel : ViewModels.BaseViewModel;
        Task RemoveLastFromBackStackAsync();  
        Task RemoveBackStackAsync();
    }
}