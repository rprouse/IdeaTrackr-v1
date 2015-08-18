using IdeaTrackr.Models;
using System.Collections.ObjectModel;

namespace IdeaTrackr.ViewModels
{
    public class IdeaListViewModel : BaseViewModel
    {
        private ObservableCollection<Idea> _ideas;

        public ObservableCollection<Idea> Ideas
        {
            get { return _ideas; }
            set
            {
                _ideas = value;
                NotifyPropertyChanged();
            }
        }

        public void Load()
        {
            if (Ideas != null)
                return;

            Loading = true;

            App.GetIdeaService().GetIdeas().ContinueWith(r =>
            {
                if(r.Exception == null)
                {
                    var ideaResults = r.Result;
                    Ideas = new ObservableCollection<Idea>(ideaResults);
                }
                else
                {
                    // TODO: Raise an error
                }
                Loading = false;
            });

        }
    }
}
