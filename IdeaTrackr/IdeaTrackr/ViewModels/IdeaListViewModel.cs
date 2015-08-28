using IdeaTrackr.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class IdeaListViewModel : BaseViewModel
    {
        ObservableCollection<Idea> _ideas;

        public IdeaListViewModel(INavigation navigation) : base(navigation)
        {
            RefreshCommand = new Command(async () => await LoadAsync(), () => !Loading );
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

        public ICommand RefreshCommand { get; private set; }

        public async Task LoadAsync()
        {
            if (Loading)
                return;

            await PerformNetworkOperationAsync(async () =>
            {
                var service = await App.GetIdeaServiceAsync();
                var ideas = await service.GetIdeasAsync();
                Ideas = new ObservableCollection<Idea>(ideas);
            });
        }
    }
}
