using System;
using Xamarin.Forms;
using IdeaTrackr.Interfaces;
using IdeaTrackr.Models;
using IdeaTrackr.Services;

namespace IdeaTrackr.ViewModels
{
    public class BaseViewModel : BaseNotifyPropertyChanged
    {
        bool _loading;

        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Subscribe<ILoading>(Messages.Loading, (sender) => Loading = sender.Loading);
        }

        /// <summary>
        /// Subscribe to receive messages from the MessagingCenter
        /// </summary>
        /// <typeparam name="TSender"></typeparam>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        protected void Subscribe<TSender>(string message, Action<TSender> callback) where TSender : class
        {
            MessagingCenter.Subscribe(this, message, callback);
        }

        public INavigation Navigation { get; set; }

        public bool Loading
        {
            get { return _loading; }
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
