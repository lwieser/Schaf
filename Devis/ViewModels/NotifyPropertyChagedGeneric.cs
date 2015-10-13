using Devis.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Devis.ViewModels
{
    public class NotifyPropertyChangedGeneric : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region  INotifyPropertyChanged

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
