using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThePongMobile.Models;
using ThePongMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace ThePongMobile.Views
{
    public partial class SetupPage : ContentPage
    {
        public SetupPage()
        {
            BindingContext = new SetupPageViewModel(new PageService());
            InitializeComponent();
        }
    }
}