using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Wimika.MoneyGuard.Core.Types;

namespace MoneyGuardSdkExample
{
    public class SessionHolder : INotifyPropertyChanged
    {
        private IBasicSession _session;
        public SessionHolder()
        {
            SessionInstance.SessionChanged += (sender, session ) =>
            {
                Session = session;
            };
        }

        public IBasicSession Session
        {
            get { return _session; }
            set
            {
               SetProperty(ref _session, value, "Session") ;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
    }
}
