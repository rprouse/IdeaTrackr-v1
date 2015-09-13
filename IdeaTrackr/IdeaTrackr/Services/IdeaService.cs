using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;
using IdeaTrackr.Interfaces;
using IdeaTrackr.Models;

namespace IdeaTrackr.Services
{
    public class IdeaService
    {
        MobileServiceClient _client;
        IMobileServiceSyncTable<Idea> _table;
        ILoginProvider _loginProvider;

        public IdeaService()
        {
            _loginProvider = DependencyService.Get<ILoginProvider>();
            Ideas = new ObservableCollection<Idea>();
        }

        public ObservableCollection<Idea> Ideas { get; private set; }

        internal async Task InitAsync()
        {
            var store = new MobileServiceSQLiteStore("ideas.db3");
            store.DefineTable<Idea>();

            _client = new MobileServiceClient("https://ideatrackr.azure-mobile.net/", "QIkEbdMIlHQGiTNdqxOigcNHUBPGzO64");

            await _client.SyncContext.InitializeAsync(store);
            _table = _client.GetSyncTable<Idea>();
        }

        public MobileServiceClient MobileServiceClient => _client;

        public MobileServiceUser CurrentUser
        {
            get { return _client.CurrentUser; }
            set { _client.CurrentUser = value; }
        }
        public bool LoggedIn => CurrentUser != null && !string.IsNullOrWhiteSpace(CurrentUser.UserId);

        public async Task SyncAsync()
        {
            await PerformNetworkOperationAsync(async () =>
            {
                await InternalSyncAsync();
                return true;
            });
        }

        async Task InternalSyncAsync()
        {
            await _client.SyncContext.PushAsync();
            await _table.PullAsync("Ideas", _table.CreateQuery());
        }

        public async Task<IEnumerable<Idea>> GetIdeasAsync() => await _table.ReadAsync();

        public async Task<Idea> GetIdeaAsync(string id) =>
            await PerformNetworkOperationAsync(async () =>
            {
                await _table.PullAsync("Idea", _table.Where(i => i.Id == id));
                return await _table.LookupAsync(id);
            });

        public async Task<Idea> SaveIdeaAsync(Idea idea) =>
            await PerformNetworkOperationAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(idea.Id))
                    await _table.InsertAsync(idea);
                else
                    await _table.UpdateAsync(idea);
                await InternalSyncAsync();
                return idea;
            });

        public async Task DeleteIdeaAsync(Idea idea)
        {
            await PerformNetworkOperationAsync(async () =>
            {
                await _table.DeleteAsync(idea);
                await InternalSyncAsync();
                return true;
            });
        }

        public async Task Login(MobileServiceAuthenticationProvider provider)
        {
            var user = await _loginProvider.LoginAsync(MobileServiceClient, provider);
            CurrentUser = user;
            var cache = new LoginToken(user, provider);
            await cache.Persist();
        }

        public async Task Logout()
        {
            await LoginToken.Delete();
            CurrentUser = null;
            ShowLogin();
        }

        public async Task EnsureLoggedIn()
        {
            if (!LoggedIn)
            {
                var token = await LoginToken.Load();
                if (token != null)
                {
                    CurrentUser = token.User;

                    // If not authorized, this will log the user out
                    await InternalSyncAsync();
                }

                // If not logged in from cache, show login
                if (!LoggedIn)
                    ShowLogin();
            }
            if (LoggedIn)
                MessagingCenter.Send(this, Messages.LoggedIn);
        }

        void ShowLogin()
        {
            MessagingCenter.Send(this, Messages.ShowLogin);
        }

        public async Task LoadIdeasAsync()
        {
            await PerformNetworkOperationAsync(async () =>
            {
                IIdeaProvider ideas = new IdeaProvider(await GetIdeasAsync());
                MessagingCenter.Send(ideas, Messages.IdeasLoaded);
                return true;
            });
        }

        async Task<TReturn> PerformNetworkOperationAsync<TReturn>(Func<Task<TReturn>> func)
        {
            MessagingCenter.Send<ILoading>(new LoadingMessage(true), Messages.Loading);

            try
            {
                return await func();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                if (msioe.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Debug.WriteLine($"Authentication Failed: {msioe.Message}");
                    await Logout();
                }
                else
                {
                    Debug.WriteLine($"INVALID: {msioe.Message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }

            MessagingCenter.Send<ILoading>(new LoadingMessage(false), Messages.Loading);

            return default(TReturn);
        }
    }
}
