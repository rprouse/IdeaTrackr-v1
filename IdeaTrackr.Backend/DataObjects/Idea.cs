using Microsoft.WindowsAzure.Mobile.Service;
using System;

namespace IdeaTrackr.Backend.DataObjects
{
    public class Idea : EntityData
    {
        public string Name { get; set; }

        public string Problem { get; set; }

        public string Solution { get; set; }

        public string Notes { get; set; }

        public int Rating { get; set; }

        public string UserId { get; set; }
    }
}