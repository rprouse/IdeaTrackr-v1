using IdeaTrackr.Services;
using IdeaTrackr.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IdeaTrackr
{
    public class App : Application
    {
        private static IdeaService _service;

        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new IdeaListView());
        }

        public static async Task<IdeaService> GetIdeaServiceAsync()
        {
            if(_service == null)
            {
                _service = new IdeaService();
                await _service.InitAsync();
            }
            return _service;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
