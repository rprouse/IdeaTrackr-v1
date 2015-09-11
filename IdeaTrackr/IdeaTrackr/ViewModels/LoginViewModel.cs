using IdeaTrackr.Services;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IdeaTrackr.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        internal const string LoggedInMessage = "LoggedIn";

        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            FacebookLoginCommand = new Command(async () => await LoginAsync(MobileServiceAuthenticationProvider.Facebook));
            TwitterLoginCommand = new Command(async () => await LoginAsync(MobileServiceAuthenticationProvider.Twitter));
            GoogleLoginCommand = new Command(async () => await LoginAsync(MobileServiceAuthenticationProvider.Google));
        }

        public ICommand FacebookLoginCommand { get; }

        public ICommand TwitterLoginCommand { get; }

        public ICommand GoogleLoginCommand { get; }

        async Task LoginAsync(MobileServiceAuthenticationProvider provider)
        {
            var service = await App.GetIdeaServiceAsync();
            await service.Login(provider);

            // Handle case where login failed
            if (App.LoggedIn)
            {
                // If not authorized, this will log the user out
                await service.SyncAsync();
                
                if (App.LoggedIn)
                {
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send(this, LoggedInMessage);
                }
            }
        }
    }
}
