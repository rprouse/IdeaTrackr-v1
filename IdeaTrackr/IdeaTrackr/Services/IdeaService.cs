using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdeaTrackr.Models;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Diagnostics;
using System;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Akavache;

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
        }

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
            try
            {
                await _client.SyncContext.PushAsync();
                await _table.PullAsync("Ideas", _table.CreateQuery());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                if (msioe.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Debug.WriteLine($"Authentication Failed: {msioe.Message}");
                    CurrentUser = null;
                }
                else
                {
                    Debug.WriteLine($"INVALID: {msioe.Message}");
                }
            }
            // TODO: Log out on forbidden
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Idea>> GetIdeasAsync()
        {
            //await SyncAsync();
            return await _table.ReadAsync();
        }

        public async Task<Idea> GetIdeaAsync(string id)
        {
            await _table.PullAsync("Idea", _table.Where(i => i.Id == id));
            return await _table.LookupAsync(id);
        }

        public async Task<Idea> SaveIdeaAsync(Idea idea)
        {
            if (string.IsNullOrWhiteSpace(idea.Id))
                await _table.InsertAsync(idea);
            else
                await _table.UpdateAsync(idea);
            await SyncAsync();
            return idea;
        }

        public async Task DeleteIdeaAsync(Idea idea)
        {
            await _table.DeleteAsync(idea);
            await SyncAsync();
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
        }
    }
}
