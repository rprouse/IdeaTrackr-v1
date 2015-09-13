using IdeaTrackr.Interfaces;
using IdeaTrackr.Models;
using System.Collections.Generic;

namespace IdeaTrackr.Services
{
    public class IdeaProvider: IIdeaProvider
    {
        IEnumerable<Idea> _ideas;

        public IdeaProvider(IEnumerable<Idea> ideas)
        {
            _ideas = ideas;
        }

        public void CopyIdeasInto(IList<Idea> destination)
        {
            // TODO: Merge?
            destination.Clear();
            foreach (var idea in _ideas)
                destination.Add(idea);
        }
    }
}
