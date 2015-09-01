using IdeaTrackr.Services;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(IdeaTrackr.Droid.Services.LoginProvider))]
namespace IdeaTrackr.Droid.Services
{
    public class LoginProvider : ILoginProvider
    {
        /// <summary>
        /// Logs in using the given login provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>The logged in user</returns>
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider) =>
            await client.LoginAsync(Xamarin.Forms.Forms.Context, provider);
    }
}