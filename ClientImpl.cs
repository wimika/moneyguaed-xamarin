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

namespace Moneyguard.Xamarin
{
    public class ClientImpl : Java.Lang.Object, IBasicClient
    {
        public void OnCredentialScanCompleted(long p0, Com.Wimika.Moneyguardcore.CredentialScanResult p1)
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

        public void OnTransactionCheckCompleted(long p0, Com.Wimika.Moneyguardcore.TransactionCheckResult p1)
        {
            throw new NotImplementedException();
        }

        public void OnTypingProfileMatchResult(long p0, Com.Wimika.Moneyguardcore.TypingProfileMatchingResult p1)
        {
            throw new NotImplementedException();
        }
    }
}