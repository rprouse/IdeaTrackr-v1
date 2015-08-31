using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IdeaTrackr.Services;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace IdeaTrackr.Droid
{
    public static class IdeaServiceExtensions
    {
        public static async Task Login(this IdeaService service, Context ctx)
        {
            var client = service.MobileServiceClient;
            var user = await client.LoginAsync(ctx, MobileServiceAuthenticationProvider.Google);
            service.CurrentUser = user;
        }

        public static async Task Login(this IdeaService service, string authToken)
        {
            JObject tokenObject = CreateTokenObject(authToken);

            var client = service.MobileServiceClient;
            var user = await client.LoginAsync(MobileServiceAuthenticationProvider.Google, tokenObject);
            service.CurrentUser = user;
        }

        static JObject CreateTokenObject(string authToken)
        {
            JObject tokenObject = new JObject();
            tokenObject.Add("id_token", authToken);
            return tokenObject;
        }
    }
}