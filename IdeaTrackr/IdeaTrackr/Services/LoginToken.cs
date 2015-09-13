using Akavache;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace IdeaTrackr.Services
{
    public class LoginToken
    {
        const string UserCacheKey = "UserCacheKey";
        const string TokenCacheKey = "TokenCacheKey";
        const string ProviderCacheKey = "ProviderCacheKey";
        static TimeSpan LoginExpiry = TimeSpan.FromDays(365);

        public static async Task<LoginToken> Load()
        {
            try
            {
                var user = await BlobCache.UserAccount.GetObject<string>(UserCacheKey);
                var token = await BlobCache.UserAccount.GetObject<string>(TokenCacheKey);
                var provider = await BlobCache.UserAccount.GetObject<MobileServiceAuthenticationProvider>(ProviderCacheKey);
                return new LoginToken(user, token, provider);
            }
            catch (KeyNotFoundException)
            {
            }
            catch (Exception)
            {
                await Delete();
            }
            return null;
        }

        public static async Task Delete()
        {
            await BlobCache.UserAccount.InvalidateObject<string>(UserCacheKey);
            await BlobCache.UserAccount.InvalidateObject<string>(TokenCacheKey);
            await BlobCache.UserAccount.InvalidateObject<MobileServiceAuthenticationProvider>(ProviderCacheKey);
        }

        public async Task Persist()
        {
            if(User == null)
            {
                await Delete();
                return;
            }
            await BlobCache.UserAccount.InsertObject(UserCacheKey, User.UserId, LoginExpiry);
            await BlobCache.UserAccount.InsertObject(TokenCacheKey, User.MobileServiceAuthenticationToken, LoginExpiry);
            await BlobCache.UserAccount.InsertObject(ProviderCacheKey, Provider, LoginExpiry);
        }

        public LoginToken(MobileServiceUser user, MobileServiceAuthenticationProvider provider)
        {
            if (user != null && !string.IsNullOrWhiteSpace(user.MobileServiceAuthenticationToken))
            {
                User = user;
            }
            Provider = provider;
        }

        LoginToken(string username, string token, MobileServiceAuthenticationProvider provider)
        {
            User = new MobileServiceUser(username) { MobileServiceAuthenticationToken = token };
            Provider = provider;
        }

        public MobileServiceUser User { get; set; }

        public MobileServiceAuthenticationProvider Provider { get; set; }
    }
}
