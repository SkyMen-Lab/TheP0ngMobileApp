using System.Threading.Tasks;
using ThePongMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public interface INavigationService
    {
        Task InitAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object param) where TViewModel : BaseViewModel;
        Task RemoveLastFromBackStackAsync();  
        Task RemoveBackStackAsync();

        Task PushModalAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task PreviousModalPage();

        Task PreviousPage();
        Task<bool> DisplayConfirmation(string title, string message, string ok, string cancel);
        Task DisplayInformation(string title, string message, string ok);


    }
}