using IdeaTrackr.Services;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using UIKit;

namespace IdeaTrackr.iOS.Services
{
    public class LoginProvider : ILoginProvider
    {
        UIViewController _controller;

        public LoginProvider(UIViewController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Logs in using the given login provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>The logged in user</returns>
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider) =>
            await client.LoginAsync(_controller, provider);

        /// <summary>
        /// Logs in to the given provider using the cached token
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, JObject token) =>
            await client.LoginAsync(provider, tokenObject);
    }
}
