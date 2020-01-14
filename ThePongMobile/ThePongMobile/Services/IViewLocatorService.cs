using ThePongMobile.ViewModels;
using ThePongMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public interface IViewLocatorService
    {
        Page GetPageFromViewModel<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;
    }
}