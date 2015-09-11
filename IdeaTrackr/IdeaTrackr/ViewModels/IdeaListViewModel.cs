using IdeaTrackr.Models;
using IdeaTrackr.Services;
using IdeaTrackr.Views;
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

            MessagingCenter.Subscribe<LoginViewModel>(this, LoginViewModel.LoggedInMessage, async (sender) => await LoadAsync());
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

        public ICommand RefreshCommand { get; }

        public async Task EnsureLoggedIn()
        {
            if (!App.LoggedIn)
            {
                var service = await App.GetIdeaServiceAsync();
                var token = await LoginToken.Load();
                if (token != null)
                {
                    service.CurrentUser = token.User;

                    // If not authorized, this will log the user out
                    await service.SyncAsync();
                }

                // If not logged in from cache, show login
                if (!App.LoggedIn)
                {
                    await Navigation.PushModalAsync(new LoginView());
                }
                else
                {
                    await LoadAsync();
                }
            }
        }

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

        public void AddIdea()
        {
            var idea = new Idea();
            var ideaPage = new IdeaView(idea);
            Navigation.PushAsync(ideaPage);
        }
    }
}
