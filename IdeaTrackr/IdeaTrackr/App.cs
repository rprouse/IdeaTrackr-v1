using IdeaTrackr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace IdeaTrackr
{
    public class App : Application
    {
        private static IdeaService _service;

        public App()
        {
            // The root page of your application
            MainPage = new IdeaTrackr.Views.IdeaList();
        }

        public static IdeaService GetIdeaService()
        {
            if(_service == null)
            {
                _service = new IdeaService("http://localhost:60978/");
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
