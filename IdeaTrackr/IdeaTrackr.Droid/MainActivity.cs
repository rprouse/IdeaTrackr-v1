﻿
using Android.App;
using Android.Content.PM;
using Android.OS;
using IdeaTrackr.Droid.Services;
using IdeaTrackr.Services;

namespace IdeaTrackr.Droid
{
    [Activity(Label = "Idea Trackr", MainLauncher = true, Icon = "@mipmap/ic_launcher", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);

            var app = new App(new LoginProvider(this));
            app.LoadMainPage();
            LoadApplication(app);

            //if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            //{
            //    ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));
            //}
        }

        public override void OnBackPressed()
        {
            // Prevent the user from pressing back to leave the login page
            if (!App.LoggedIn)
                return;

            base.OnBackPressed();
        }
    }
}

