using IdeaTrackr.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class IdeaListViewModel : BaseViewModel
    {
        ObservableCollection<Idea> _ideas;

        public IdeaListViewModel(INavigation navigation) : base(navigation)
        {
        }

        public ObservableCollection<Idea> Ideas
        {
            get { return _ideas; }
            set
            {
                _ideas = value;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await PerformNetworkOperationAsync(async () =>
            {
                var service = await App.GetIdeaServiceAsync();
                var ideas = await service.GetIdeasAsync();
                Ideas = new ObservableCollection<Idea>(ideas);
            });
        }
    }
}
