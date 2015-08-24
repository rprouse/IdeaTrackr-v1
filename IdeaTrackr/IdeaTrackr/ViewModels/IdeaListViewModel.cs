using IdeaTrackr.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            if (Ideas != null)
                return;

            await PerformNetworkOperationAsync(async () =>
            {
                var service = await App.GetIdeaServiceAsync();
                var ideas = await service.GetIdeasAsync();
                Ideas = new ObservableCollection<Idea>(ideas);
            });
        }

        async Task PerformNetworkOperationAsync(Func<Task> func)
        {
            Loading = true;

            try
            {
                await func();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine($"INVALID: {msioe.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }

            Loading = false;
        }
    }
}
