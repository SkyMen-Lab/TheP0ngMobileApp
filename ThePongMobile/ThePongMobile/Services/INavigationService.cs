using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public interface INavigationService
    {
        //TODO: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation 
        // proper Navigation service
        void OpenPage(Page nextPage);
        void PreviousPage();
    }
}