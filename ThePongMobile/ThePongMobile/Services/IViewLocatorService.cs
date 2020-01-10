using ThePongMobile.ViewModels;
using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public interface IViewLocatorService
    {
        Page GetPageFromViewModel<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;
    }
}