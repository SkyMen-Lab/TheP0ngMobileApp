using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThePongMobile.Services;
using Xamarin.Forms;

namespace ThePongMobile.Services
{
    public class NavigationService : INavigationService
    {
        //public void OpenPage(Page nextPage)
        //{
        //    throw new NotImplementedException();
        //}

        //public void PreviousPage()
        //{
        //    throw new NotImplementedException();
        //}

           
        public async Task PreviousPage() 
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
