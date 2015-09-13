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

            string addIcon = null;
            switch(Device.OS)
            {
                case TargetPlatform.Android:
                    addIcon = "ic_menu_add";
                    break;
                case TargetPlatform.WinPhone:
                    addIcon = "add.png";
                    break;
            }
            ToolbarItems.Add(new ToolbarItem("Add", addIcon,
                async () => await _viewModel.AddIdea(), ToolbarItemOrder.Primary));
            ToolbarItems.Add(new ToolbarItem("Logout", null,
                async () => await _viewModel.Logout(), ToolbarItemOrder.Secondary));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearing();
        }

        public async void OnIdeaTapped(object sender, ItemTappedEventArgs e)
        {
            await _viewModel.ShowIdeaView(e.Item as Idea);
        }
    }
}
