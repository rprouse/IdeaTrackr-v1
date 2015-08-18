using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using IdeaTrackr.Backend.DataObjects;
using IdeaTrackr.Backend.Models;

namespace IdeaTrackr.Backend.Controllers
{
    public class IdeaController : TableController<Idea>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Idea>(context, Request, Services);
        }

        // GET tables/Idea
        public IQueryable<Idea> GetAllIdeas() => Query();

        // GET tables/Idea/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Idea> GetIdea(string id) => Lookup(id);

        // PATCH tables/Idea/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Idea> PatchIdea(string id, Delta<Idea> patch) => UpdateAsync(id, patch);

        // POST tables/Idea
        public async Task<IHttpActionResult> PostIdea(Idea item)
        {
            Idea current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Idea/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteIdea(string id) => DeleteAsync(id);
    }
}