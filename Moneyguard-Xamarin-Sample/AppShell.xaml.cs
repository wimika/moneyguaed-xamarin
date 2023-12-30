using Moneyguard_Xamarin_Sample.ViewModels;
using Moneyguard_Xamarin_Sample.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Moneyguard_Xamarin_Sample
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
