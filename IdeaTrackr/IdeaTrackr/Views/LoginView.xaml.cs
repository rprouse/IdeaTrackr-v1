using IdeaTrackr.ViewModels;

using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public partial class LoginView : ContentPage
    {
        LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel(Navigation);
            BindingContext = _viewModel;
        }
    }
}
