using IdeaTrackr.Interfaces;

namespace IdeaTrackr.Services
{
    public class LoadingMessage : ILoading
    {
        public LoadingMessage(bool loading)
        {
            Loading = Loading;
        }

        /// <summary>
        /// True if we are busy loading data
        /// </summary>
        public bool Loading { get; private set; }
    }
}
