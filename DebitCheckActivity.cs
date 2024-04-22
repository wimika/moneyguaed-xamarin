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
using Wimika.MoneyGuard.Core.Types;

namespace AndroidTestApp
{
    [Activity(Label = "DebitCheckActivity")]
    public class DebitCheckActivity : Activity
    {
        private EditText sourceAccount;
        private EditText destinationBank;
        private EditText destinationAccount;
        private EditText memo;
        private EditText amount;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.debit_check);



            var buttonCheckDebit = FindViewById(Resource.Id.buttonCheckDebit);
            buttonCheckDebit.Click += DebitCheckClick;

            var buttonDebitGoback = FindViewById(Resource.Id.buttonDebitGoback);
            buttonDebitGoback.Click += GoBackClick;

            amount = FindViewById<EditText>(Resource.Id.editTextAmount);
            memo = FindViewById<EditText>(Resource.Id.editTextMemo);
            destinationAccount = FindViewById<EditText>(Resource.Id.editTextDestinationAccount);
            destinationBank = FindViewById<EditText>(Resource.Id.editTextDestinationBank);
            sourceAccount = FindViewById<EditText>(Resource.Id.editTextSource);
        }

        private async void DebitCheckClick(object sender, EventArgs eventArgs)
        {
            var result = await SessionHolder.Session.CheckDebitTransaction(
                new DebitTransaction
                {
                    Amount = double.Parse(amount.Text),
                    SourceAccountNumber = sourceAccount.Text,
                    DestinationAccountNumber = destinationAccount.Text,
                    DestinationBank = destinationBank.Text,
                    Memo = memo.Text
                }
                );
 

            Toast.MakeText(this, "Debit Transaction status is " + SessionHolder.StatusAsString(result.Status), ToastLength.Long).Show();
        }

        private async void GoBackClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(ChoosingActivity));
        }
    }
}