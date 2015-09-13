using IdeaTrackr.Models;
using System.Collections.Generic;

namespace IdeaTrackr.Interfaces
{
    public interface IIdeaProvider
    {
        void CopyIdeasInto(IList<Idea> destination);
    }
}
