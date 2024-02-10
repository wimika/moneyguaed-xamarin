using System;
using System.Collections.Generic;
using System.Text;
using Wimika.MoneyGuard.Core.Types;

namespace MoneyGuardSdkExample
{
    public class SessionInstance
    {
        public static EventHandler<IBasicSession> SessionChanged; 

        public static void SetSession(IBasicSession session)
        {
            SessionChanged?.Invoke(null, session);
        }
    }
}
