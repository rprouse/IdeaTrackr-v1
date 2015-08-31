using IdeaTrackr.Services;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IdeaTrackr.WinPhone.Services
{
    public class LoginProvider : ILoginProvider
    {
        /// <summary>
        /// Logs in using the given login provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>The logged in user</returns>
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider) =>
            await client.LoginAsync(provider);

        /// <summary>
        /// Logs in to the given provider using the cached token
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, JObject token) =>
            await client.LoginAsync(provider, token);
    }
}
