using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdeaTrackr.Models;

namespace IdeaTrackr.Services
{
    public class IdeaService
    {
        private static MobileServiceClient _client;

        public IdeaService(string serviceBaseUri)
        {
            _client = new MobileServiceClient("http://localhost:60978/", "IdeaTrackrKey");
        }

        public async Task<IEnumerable<Idea>> GetIdeas()
        {
            var table = _client.GetTable<Idea>();
            return await table.ReadAsync();
        }

        public async Task<Idea> AddOrUpdateIdea(Idea idea)
        {
            var table = _client.GetTable<Idea>();
            if(string.IsNullOrWhiteSpace(idea.Id))
                await table.InsertAsync(idea);
            else
                await table.UpdateAsync(idea);
            return idea;
        }
    }
}
