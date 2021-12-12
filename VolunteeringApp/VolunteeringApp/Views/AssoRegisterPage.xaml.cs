﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VolunteeringApp.ViewModels;

namespace VolunteeringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssoRegisterPage : ContentPage
    {
        public AssoRegisterPage()
        {
            this.BindingContext = new AssoRegisterViewModel();
            InitializeComponent();
        }
    }
}