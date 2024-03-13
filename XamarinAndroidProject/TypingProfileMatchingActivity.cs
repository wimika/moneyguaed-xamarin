using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Wimika.Moneyguardcore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wimika.MoneyGuard.Core.Types;

namespace AndroidTestApp
{
    [Activity(Label = "TypingProfileMatchingActivity")]
    public class TypingProfileMatchingActivity : Activity
    {
        private EditText editTextFragment;
        private TextView editFragment;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.typing_profile_check);

            // Create your application here
            var buttonCheckTypingProfile = FindViewById(Resource.Id.buttonCheckTypingProfile);
            buttonCheckTypingProfile.Click += CheckTypingProfileClick;

            var buttonTypingGoback = FindViewById(Resource.Id.buttonTypingGoback);
            buttonTypingGoback.Click += GoBackClick;

            editTextFragment = FindViewById<EditText>(Resource.Id.editTextFragment);
            editFragment = FindViewById<TextView>(Resource.Id.textFragment);
            editFragment.Text = SessionHolder.Session.TypingProfileRecorder.TypingFragment;

            editTextFragment.KeyPress += EditTextFragment_KeyPress;
            editTextFragment.AfterTextChanged += EditTextFragment_AfterTextChanged;
        }

        private void EditTextFragment_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            SessionHolder.Session.TypingProfileRecorder.OnTextChanged(editTextFragment.Text);
        }

        private void EditTextFragment_KeyPress(object sender, View.KeyEventArgs e)
        {
            SessionHolder.Session.TypingProfileRecorder.OnKeydown();
            e.Handled = false;
        }

        private async void GoBackClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(ChoosingActivity));
        }

        private async void CheckTypingProfileClick(object sender, EventArgs eventArgs)
        {
            var typingProfileMatchingResult = await SessionHolder.Session.TypingProfileRecorder.MatchTypingProfile();
            var message = "Not Enrolled";

            if (typingProfileMatchingResult.IsEnrolledOnThisDevice)
            {
                if (typingProfileMatchingResult.Matched)
                {
                    if (typingProfileMatchingResult.HighConfidence)
                    {
                        message = "Enrolled and Matched With High Confidence";
                    }
                    else
                    {
                        message = "Enrolled and Matched With Low Confidence";
                    }
                }
                else
                {
                    //typing profile did not match, do not proceed
                    message = "Not matched";
                }
            }

            Toast.MakeText(
                this,
                message,
                ToastLength.Long
                ).Show();
        }
    }


}