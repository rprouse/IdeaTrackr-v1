using IdeaTrackr.Models;
using IdeaTrackr.ViewModels;
using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public partial class IdeaView : ContentPage
    {
        public IdeaView() : this(new Idea())
        {
        }

        public IdeaView(Idea idea)
        {
            InitializeComponent();

            BindingContext = new IdeaViewModel(Navigation, idea);
        }
    }
}
