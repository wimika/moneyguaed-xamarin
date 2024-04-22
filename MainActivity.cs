using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Wimika.MoneyGuard.Core.Android;
using System.Threading.Tasks;
using Android.Widget;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Wimika.MoneyGuard.Core.Types;

namespace AndroidTestApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView text; 

        private const int WIMIKA_XAMARIN_BANK = 101;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            text = (TextView)FindViewById(Resource.Id.textViewWarning);


            var button = FindViewById(Resource.Id.buttonProceed);
            button.Click += ProceedClick;
             


            var s = await MoneyGuardSdk.Startup(this); 

            if(s.MoneyGuardActive)
            {
                var risky = false;
                foreach(var r in s.Risks)
                {
                    if(r.Status != RiskStatus.RISK_STATUS_SAFE)
                    {
                        risky = true;
                        break;
                    }
                }
                text.Text = risky ? "The WiFi network you are connected to is not secure. Your digital banking activities may be compromised if you proceed with logging in and transacting" : "Your Wifi Connection is Secure";
            }
            else
            {
                text.Text = "Moneyguard not active";
            }

            

        }

        private async void ProceedClick(object sender, EventArgs eventArgs)
        {
           

            StartActivity(typeof(LogInActivity));

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
