using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace IdeaTrackr.Services
{
    public interface ILoginProvider
    {
        /// <summary>
        /// Logs in using the given login provider
        /// </summary>
        /// <param name="client"></param>
        /// <param name="provider"></param>
        /// <returns>The logged in user</returns>
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider);

        /// <summary>
        /// Logs in to the given provider using the cached token
        /// </summary>
        /// <param name="client"></param>
        /// <param name="provider"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, JObject token);
    }
}
