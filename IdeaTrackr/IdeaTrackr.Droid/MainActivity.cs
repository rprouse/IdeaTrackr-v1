
using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;

namespace IdeaTrackr.Droid
{
    [Activity(Label = "Idea Trackr", Icon = "@mipmap/ic_launcher", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            var app = new App();
            app.LoadMainPage();
            LoadApplication(app);

            //if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            //{
            //    ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));
            //}
        }
    }
}

