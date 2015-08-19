using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using IdeaTrackr.Backend.DataObjects;
using IdeaTrackr.Backend.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace IdeaTrackr.Backend
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : ClearDatabaseSchemaIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<Idea> ideas = new List<Idea>
            {
                new Idea { Id = Guid.NewGuid().ToString(), Name = "Idea Trackr", Problem = "It is hard to come up with and validate business ideas", Solution = "Android application to enter, track, rate and validate business ideas. Need to come up with a process for rating and validating.", Notes = "I should try building this in Xamarin.Forms as an experiment", Rating = 4 },
                new Idea { Id = Guid.NewGuid().ToString(), Name = "Goal Trackr", Problem = "People are very bad at sticking to their goals and resolutions", Solution = "Track short, medium and long term goals\n\nSet,  track, remind and manage", Notes = "I'm not really a goal oriented person, could I be passionate about this project?", Rating = 3 },
                new Idea { Id = Guid.NewGuid().ToString(), Name = "Domain Stormer", Problem = "It is hard to find a short, unused domain name", Solution = "Search for and purchase domains by entering two or three words, then work with all the combinations and synonyms.", Notes = "Too much competition", Rating = 2 },
            };

            foreach (Idea todoItem in ideas)
            {
                context.Set<Idea>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

