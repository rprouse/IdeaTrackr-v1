using IdeaTrackr.Services;
using IdeaTrackr.Styles;
using IdeaTrackr.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IdeaTrackr
{
    public class App : Application
    {
        static IdeaService _service;

        static ILoginProvider _loginProvider;

        public App(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;

            // The root page of your application
            MainPage = new ContentPage();
        }

        public void LoadMainPage()
        {
            MainPage = new NavigationPage(new IdeaListView())
            {
                BarBackgroundColor = StyleKit.DarkPrimaryColor,
                BarTextColor = StyleKit.HeaderTextColor
            };
        }

        public static bool LoggedIn => _service != null && _service.LoggedIn;

        public string ResumeAtIdeaId { get; set; }

        public static async Task<IdeaService> GetIdeaServiceAsync()
        {
            if(_service == null)
            {
                _service = new IdeaService(_loginProvider);
                await _service.InitAsync();
            }
            return _service;
        }

        protected async override void OnStart()
        {
            Debug.WriteLine("OnStart");

            // always re-set when the app starts
            // users expect this (usually)
            if (Properties.ContainsKey("ResumeAtIdeaId"))
            {
                var id = Properties["ResumeAtIdeaId"].ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    Debug.WriteLine("   id = " + id);
                    ResumeAtIdeaId = id;

                    var service = await GetIdeaServiceAsync();
                    var idea = await service.GetIdeaAsync(ResumeAtIdeaId);
                    var ideaView = new IdeaView(idea);

                    await MainPage.Navigation.PushAsync(ideaView, false); // no animation
                }
            }
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
