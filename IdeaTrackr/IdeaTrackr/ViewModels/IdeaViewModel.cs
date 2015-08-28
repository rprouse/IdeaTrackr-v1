using IdeaTrackr.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class IdeaViewModel : BaseViewModel
    {
        Idea _idea;

        public IdeaViewModel(INavigation navigation, Idea idea) : base(navigation)
        {
            Idea = idea;

            SaveCommand = new Command(async () =>
            {
                await PerformNetworkOperationAsync(async () =>
                {
                    var service = await App.GetIdeaServiceAsync();
                    await service.SaveIdeaAsync(Idea);
                });
                await Navigation.PopAsync();
            });

            DeleteCommand = new Command(async () =>
            {
                await PerformNetworkOperationAsync(async () =>
                {
                    var service = await App.GetIdeaServiceAsync();
                    await service.DeleteIdeaAsync(Idea);
                });
                await Navigation.PopAsync();
            });

            CancelCommand = new Command(() =>
            {
                Navigation.PopAsync();
            });
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

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }
    }
}
