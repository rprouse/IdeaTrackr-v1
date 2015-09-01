using Microsoft.WindowsAzure.MobileServices;
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
    }
}
