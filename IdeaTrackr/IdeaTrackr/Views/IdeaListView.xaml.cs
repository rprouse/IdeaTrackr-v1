using IdeaTrackr.Models;
using IdeaTrackr.ViewModels;
using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public partial class IdeaListView : ContentPage
    {
        IdeaListViewModel _viewModel;

        public IdeaListView()
        {
            InitializeComponent();
            _viewModel = new IdeaListViewModel(Navigation);

            BindingContext = _viewModel;

            #region toolbar
            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("Add", null, _viewModel.AddIdea, ToolbarItemOrder.Primary, 0);
            }
            else if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("Add", "ic_menu_add", _viewModel.AddIdea, ToolbarItemOrder.Primary, 0);
            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                tbi = new ToolbarItem("Add", "add.png", _viewModel.AddIdea, ToolbarItemOrder.Primary, 0);
            }

            ToolbarItems.Add(tbi);
            #endregion
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtIdeaId = "";
            await _viewModel.EnsureLoggedIn();
        }

        public void OnIdeaTapped(object sender, ItemTappedEventArgs e)
        {
            // Display detail page
            var idea = e.Item as Idea;
            Navigation.PushAsync(new IdeaView(idea));
        }
    }
}
