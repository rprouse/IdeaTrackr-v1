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

namespace IdeaTrackr.Droid
{
    [Activity(Label = "Login", Icon = "@mipmap/ic_launcher", Theme = "@style/MyTheme", MainLauncher = true, NoHistory = true)]
    public class LoginActivity : Activity
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            var service = await App.GetIdeaServiceAsync();
            await service.Login(this);

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}