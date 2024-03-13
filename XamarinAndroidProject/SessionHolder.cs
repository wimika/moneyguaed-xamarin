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
    internal static class SessionHolder
    {
        internal static IBasicSession Session { get; set; }

        internal static string StatusAsString(RiskStatus status)
        {
            switch (status)
            { 
                    
                case RiskStatus.RISK_STATUS_SAFE:
                    return "Safe";
                case RiskStatus.RISK_STATUS_WARN:
                    return "Warn";
                case RiskStatus.RISK_STATUS_UNSAFE:
                    return "Unsafe";
            }

            return "Unknown";
        }
    }
}