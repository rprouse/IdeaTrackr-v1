using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using IdeaTrackr.Backend.DataObjects;
using IdeaTrackr.Backend.Models;
using System.Net;

namespace IdeaTrackr.Backend.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class IdeaController : TableController<Idea>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Idea>(context, Request, Services);
        }

        // GET tables/Idea
        public IQueryable<Idea> GetAllIdeas() => Query().Where(i => i.UserId == ServiceUser.Id);

        // GET tables/Idea/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public SingleResult<Idea> GetIdea(string id) => Lookup(id);

        // PATCH tables/Idea/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Idea> PatchIdea(string id, Delta<Idea> patch)
        {
            var idea = patch.GetEntity();
            if (idea.UserId != ServiceUser.Id)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            return UpdateAsync(id, patch);
        }

        // POST tables/Idea
        public async Task<IHttpActionResult> PostIdea(Idea item)
        {
            item.UserId = ServiceUser.Id;
            Idea current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Idea/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task DeleteIdea(string id)
        {
            var result = await LookupAsync(id);
            var idea = result.Queryable.FirstOrDefault();
            if (idea.UserId == ServiceUser.Id)
            {
                await DeleteAsync(id);
            }
        }

        ServiceUser ServiceUser => User as ServiceUser;
    }
}