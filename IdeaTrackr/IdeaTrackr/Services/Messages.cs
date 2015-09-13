namespace IdeaTrackr.Services
{
    public static class Messages
    {
        /// <summary>
        /// Indicates that the UI should show a login dialog
        /// </summary>
        public const string ShowLogin = "ShowLogin";

        /// <summary>
        /// Indicates that we have logged in successfully
        /// </summary>
        public const string LoggedIn = "LoggedIn";

        /// <summary>
        /// We have started or finished loading data
        /// </summary>
        public const string Loading = "Loading";

        /// <summary>
        /// Indicates that ideas have been loaded from the server
        /// </summary>
        public const string IdeasLoaded = "IdeasLoaded";
    }
}
