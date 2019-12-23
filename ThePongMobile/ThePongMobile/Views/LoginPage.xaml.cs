﻿using Newtonsoft.Json;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            BindingContext = new LoginPageViewModel(new PageService());
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}