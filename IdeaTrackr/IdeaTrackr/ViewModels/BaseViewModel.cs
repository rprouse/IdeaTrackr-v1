using IdeaTrackr.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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

        protected async Task PerformNetworkOperationAsync(Func<Task> func)
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
