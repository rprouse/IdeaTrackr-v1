namespace IdeaTrackr.Interfaces
{
    public interface ILoading
    {
        /// <summary>
        /// True if we are busy loading data
        /// </summary>
        bool Loading { get; }
    }
}
