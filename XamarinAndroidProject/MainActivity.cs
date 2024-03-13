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

namespace AndroidTestApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText usernameEditText;
        private EditText passwordEditText;

        private const int WIMIKA_XAMARIN_BANK = 101;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);



            var button = FindViewById(Resource.Id.buttonLogin);
            button.Click += LoginClick;

            usernameEditText = FindViewById<EditText>(Resource.Id.editTextUsername);
            passwordEditText = FindViewById<EditText>(Resource.Id.editTextPassword);


        }

        private async void LoginClick(object sender, EventArgs eventArgs)
        {
            var response = await new LoginService().Session(usernameEditText.Text, passwordEditText.Text);
           
            try
            {
                var session = await MoneyGuardSdk.Register(this, WIMIKA_XAMARIN_BANK, response.Data.SessionId);
                Console.WriteLine("InstallationId -> " + session.InstallationId);
                Console.WriteLine("SessionId -> " + session.SessionId);
                SessionHolder.Session = session;
                StartActivity(typeof(ChoosingActivity));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                Console.WriteLine(ex.ToString());
            }



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

        //private async void  FabOnClick(object sender, EventArgs eventArgs)
        //{
        //    View view = (View) sender;
        //    //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
        //    //    .SetAction("Action", (View.IOnClickListener)null).Show();

        //    await Task.Factory.StartNew(
        //         async () =>
        //         {
        //            var partnerBankId = 102;
        //            var sessionToken = "R002755023";
        //             try
        //             {
        //                 var session = await MoneyGuardSdk.Register(this, partnerBankId, sessionToken);
        //                 Console.WriteLine("InstallationId -> " + session.InstallationId);
        //                 Console.WriteLine("SessionId -> " + session.SessionId);
        //             }
        //             catch (Exception ex)
        //             {
        //                 Console.WriteLine(ex.ToString());
        //             }
                    
        //        }
        //       );

        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
