using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaTrackr.Models
{
    public class Idea
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Problem { get; set; }

        public string Solution { get; set; }

        public string Notes { get; set; }

        public int Rating { get; set; }
    }
}
