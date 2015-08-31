using IdeaTrackr.ViewModels;

using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
    }
}
