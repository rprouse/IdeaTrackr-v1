using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool _loading;

        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        INavigation Navigation { get; set; }

        public bool Loading
        {
            get { return _loading; }
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
