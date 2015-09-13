using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using IdeaTrackr.Services;

namespace IdeaTrackr.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
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
            if (service.LoggedIn)
            {
                // If not authorized, this will log the user out
                await service.SyncAsync();

                if (service.LoggedIn)
                {
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send(service, Messages.LoggedIn);
                }
            }
        }
    }
}
