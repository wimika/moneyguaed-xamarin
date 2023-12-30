using Moneyguard_Xamarin_Sample.Services;
using Moneyguard_Xamarin_Sample.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Moneyguard_Xamarin_Sample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
