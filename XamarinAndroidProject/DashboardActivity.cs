using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using Wimika.MoneyGuard.Application;

namespace AndroidTestApp
{
    [Activity(Label = "Dashboard", Theme = "@style/AppTheme.NoActionBar")]
    public class DashboardActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.dashboard_layout);

            var btnEnableMoneyguard = FindViewById<Button>(Resource.Id.btn_enable_moneyguard);
            btnEnableMoneyguard.Click += EnableMoneyGuard; 
            var btnExploreMoneyguard = FindViewById<Button>(Resource.Id.btn_explore_moneyguard);
            btnExploreMoneyguard.Click += ExploreMoneyGuard;

            btnEnableMoneyguard.Visibility = (bool)!MoneyGuardApp.IsInstalled(this) ? ViewStates.Visible : ViewStates.Invisible;
            btnExploreMoneyguard.Visibility = (bool)MoneyGuardApp.IsInstalled(this) ? ViewStates.Visible : ViewStates.Invisible;
        }

        private async void EnableMoneyGuard(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(EnableMoneyguardActivity));
            //await Wimika.MoneyGuard.Application.MoneyGuardApp.Install();
        }

        private async void ExploreMoneyGuard(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(ChoosingActivity));
        }
    }
}