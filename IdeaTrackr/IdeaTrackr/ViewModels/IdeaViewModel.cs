using IdeaTrackr.Models;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class IdeaViewModel : BaseViewModel
    {
        Idea _idea;

        public IdeaViewModel(INavigation navigation, Idea idea) : base(navigation)
        {
            Idea = idea;
        }

        public Idea Idea
        {
            get { return _idea; }
            set
            {
                if (_idea != value)
                {
                    _idea = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
