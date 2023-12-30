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
using Com.Wimika.Moneyguardcore;
using Java.Interop;

namespace Moneyguard_Xamarin_Sample.Droid
{
    internal class BasicClientImpl : Java.Lang.Object,  IBasicClient
    {

        public void OnCredentialScanCompleted(long p0, CredentialScanResult p1)
        {
            throw new NotImplementedException();
        }

        public void OnSessionCreated(IBasicSession p0)
        {
            throw new NotImplementedException();
        }

        public void OnSessionExpired(string p0)
        {
            throw new NotImplementedException();
        }

        public void OnTransactionCheckCompleted(long p0, TransactionCheckResult p1)
        {
            throw new NotImplementedException();
        }

        public void OnTypingProfileMatchResult(long p0, TypingProfileMatchingResult p1)
        {
            throw new NotImplementedException();
        }
    }
}