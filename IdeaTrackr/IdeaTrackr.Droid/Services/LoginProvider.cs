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
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            return await client.LoginAsync(_context, provider);
        }

        /// <summary>
        /// Logs in to the given provider using the cached token
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, string token)
        {
            JObject tokenObject = CreateTokenObject(token);
            return await client.LoginAsync(provider, tokenObject);
        }

        static JObject CreateTokenObject(string authToken)
        {
            JObject tokenObject = new JObject();
            tokenObject.Add("id_token", authToken);
            return tokenObject;
        }
    }
}