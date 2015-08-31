using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdeaTrackr.Models;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Diagnostics;
using System;
using Newtonsoft.Json.Linq;

namespace IdeaTrackr.Services
{
    public class IdeaService
    {
        MobileServiceClient _client;
        IMobileServiceSyncTable<Idea> _table;
        ILoginProvider _loginProvider;

        public IdeaService(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;
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
        public bool LoggedIn => !string.IsNullOrWhiteSpace(CurrentUser.UserId);

        async Task SyncAsync()
        {
            try
            {
                await _client.SyncContext.PushAsync();
                await _table.PullAsync("Ideas", _table.CreateQuery());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine($"INVALID: {msioe.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Idea>> GetIdeasAsync()
        {
            await SyncAsync();
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
        }

        public async Task Login(MobileServiceAuthenticationProvider provider, string authToken)
        {
            JObject tokenObject = CreateTokenObject(provider, authToken);
            var user = await _loginProvider.LoginAsync(MobileServiceClient, provider, tokenObject);
            CurrentUser = user;
        }

        static JObject CreateTokenObject(MobileServiceAuthenticationProvider provider, string authToken)
        {
            JObject tokenObject = new JObject();
            switch (provider)
            {
                case MobileServiceAuthenticationProvider.Facebook:
                    tokenObject.Add("access_token", authToken);
                    break;
                case MobileServiceAuthenticationProvider.Google:
                    tokenObject.Add("id_token", authToken);
                    break;
                case MobileServiceAuthenticationProvider.Twitter:
                    tokenObject.Add("???", authToken);  // TODO
                    break;
            }
            return tokenObject;
        }
    }
}
