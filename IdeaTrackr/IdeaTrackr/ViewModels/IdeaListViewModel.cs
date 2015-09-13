using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using IdeaTrackr.Interfaces;
using IdeaTrackr.Models;
using IdeaTrackr.Services;
using IdeaTrackr.Views;

namespace IdeaTrackr.ViewModels
{
    public class IdeaListViewModel : BaseViewModel
    {
        public IdeaListViewModel(INavigation navigation) : base(navigation)
        {
            Ideas = new ObservableCollection<Idea>();

            RefreshCommand = new Command(async () => await RefreshAsync(), () => !Loading);

            Subscribe<IdeaService>(Messages.LoggedIn, async (sender) => await LoadAsync());
            Subscribe<IdeaService>(Messages.ShowLogin, async (sender) => await ShowLogin());
            Subscribe<IIdeaProvider>(Messages.IdeasLoaded, (sender) => sender.CopyIdeasInto(Ideas));
        }

        /// <summary>
        /// Called when the view is appearing
        /// </summary>
        /// <returns></returns>
        public async Task OnAppearing()
        {
            // reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtIdeaId = "";

            var service = await App.GetIdeaServiceAsync();
            await service.EnsureLoggedIn();
        }

        public ObservableCollection<Idea> Ideas { get; set; }

        public ICommand RefreshCommand { get; }

        public async Task RefreshAsync()
        {
            if (Loading)
                return;

            var service = await App.GetIdeaServiceAsync();
            await service.SyncAsync();
            await service.LoadIdeasAsync();
        }

        public async Task LoadAsync()
        {
            if (Loading)
                return;

            var service = await App.GetIdeaServiceAsync();
            await service.LoadIdeasAsync();
        }

        public async Task AddIdea()
        {
            await ShowIdeaView(new Idea());
        }

        public async Task Logout()
        {
            var service = await App.GetIdeaServiceAsync();
            await service.Logout();
        }

        public async Task ShowIdeaView(Idea idea)
        {
            var ideaPage = new IdeaView(idea);
            await Navigation.PushAsync(ideaPage);
        }

        async Task ShowLogin()
        {
            await Navigation.PushModalAsync(new LoginView());
        }
    }
}
