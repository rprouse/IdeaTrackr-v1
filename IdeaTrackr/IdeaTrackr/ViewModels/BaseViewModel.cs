using IdeaTrackr.Models;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class BaseViewModel : BaseNotifyPropertyChanged
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
    }
}
