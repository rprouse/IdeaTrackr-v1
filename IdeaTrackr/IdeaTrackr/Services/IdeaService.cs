using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdeaTrackr.Models;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Diagnostics;
using System;

namespace IdeaTrackr.Services
{
    public class IdeaService
    {
        MobileServiceClient _client;
        IMobileServiceSyncTable<Idea> _table;

        internal async Task InitAsync()
        {
            var store = new MobileServiceSQLiteStore("ideas.db3");
            store.DefineTable<Idea>();

            _client = new MobileServiceClient("https://ideatrackr.azure-mobile.net/", "QIkEbdMIlHQGiTNdqxOigcNHUBPGzO64");

            await _client.SyncContext.InitializeAsync(store);
            _table = _client.GetSyncTable<Idea>();
        }

        async Task SyncAsync()
        {
            try
            {
                await _client.SyncContext.PushAsync();
                await _table.PullAsync("Ideas", _table.CreateQuery());
            }
            catch(MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine($"INVALID: {msioe.Message}");
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Idea>> GetIdeasAsync()
        {
            await SyncAsync();
            return await _table.ReadAsync();
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
    }
}
