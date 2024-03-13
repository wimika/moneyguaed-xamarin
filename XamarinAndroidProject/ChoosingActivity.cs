using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidTestApp
{
    [Activity(Label = "ChoosingActivity")]
    public class ChoosingActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.choosing);



            var buttonCredentialCheck = FindViewById(Resource.Id.buttonCredentialCheck);
            buttonCredentialCheck.Click += CredentialCheckClick;
            //
            var buttonDebitCheck = FindViewById(Resource.Id.buttonDebitCheck);
            buttonDebitCheck.Click += DebitCheckClick;
            //
            var buttonGetRiskProfile = FindViewById(Resource.Id.buttonGetRiskProfile);
            buttonGetRiskProfile.Click += GetRiskProfileClick;

            var buttonTypingProfileMatching = FindViewById(Resource.Id.buttonTypingProfileMatching);
            buttonTypingProfileMatching.Click += TypingProfileMatchingClick;
            // Create your application here
        }

        private async void CredentialCheckClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(CredentialCheckActivity));
        }

        private async void GetRiskProfileClick(object sender, EventArgs eventArgs)
        {
            var result = await SessionHolder.Session.GetRiskProfile();
            Toast.MakeText(this, "Risk profile is ->" + SessionHolder.StatusAsString(result.Status), ToastLength.Long).Show();
        }

        private async void DebitCheckClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(DebitCheckActivity));
        }

        private async void TypingProfileMatchingClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof (TypingProfileMatchingActivity));
        }
    }
}