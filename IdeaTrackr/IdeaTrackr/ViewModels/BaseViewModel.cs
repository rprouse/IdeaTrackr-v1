using IdeaTrackr.Interfaces;
using IdeaTrackr.Models;
using IdeaTrackr.Services;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class BaseViewModel : BaseNotifyPropertyChanged
    {
        bool _loading;

        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;

            MessagingCenter.Subscribe<ILoading>(this, Messages.Loading,
                (sender) => Loading = sender.Loading);
        }

        public INavigation Navigation { get; set; }

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
