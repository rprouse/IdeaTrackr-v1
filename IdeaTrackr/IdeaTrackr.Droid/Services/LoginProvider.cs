using IdeaTrackr.Services;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Android.Content;

namespace IdeaTrackr.Droid.Services
{
    public class LoginProvider : ILoginProvider
    {
        Context _context;

        public LoginProvider(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Logs in using the given login provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>The logged in user</returns>
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider) =>
            await client.LoginAsync(_context, provider);

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