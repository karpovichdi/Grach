﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grach.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NonModalView : ContentPage
    {
        public NonModalView()
        {
            InitializeComponent();
        }
        
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}