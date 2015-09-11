using Akavache;
using Microsoft.Phone.Controls;

namespace IdeaTrackr.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            // Initialize Akavache
            BlobCache.ApplicationName = IdeaTrackr.App.ApplicationName;
            BlobCache.EnsureInitialized();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new IdeaTrackr.App());
        }
    }
}
