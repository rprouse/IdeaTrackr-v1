using IdeaTrackr.Models;
using IdeaTrackr.ViewModels;
using System;
using System.Linq;

using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public partial class IdeaListView : ContentPage
    {
        IdeaListViewModel _viewModel;

        public IdeaListView()
        {
            InitializeComponent();

            #region toolbar
            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("Add", null, AddIdea, ToolbarItemOrder.Primary, 0);
            }
            else if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("Add", "ic_menu_add", AddIdea, ToolbarItemOrder.Primary, 0);
            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                tbi = new ToolbarItem("Add", "add.png", AddIdea, ToolbarItemOrder.Primary, 0);
            }

            ToolbarItems.Add(tbi);
            #endregion

            _viewModel = new IdeaListViewModel(Navigation);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtIdeaId = "";
            await _viewModel.LoadAsync();
        }

        public void OnIdeaTapped(object sender, ItemTappedEventArgs e)
        {
            // Display detail page
            var idea = e.Item as Idea;
            Navigation.PushAsync(new IdeaView(idea));
        }

        void AddIdea()
        {
            var idea = new Idea();
            var ideaPage = new IdeaView(idea);
            Navigation.PushAsync(ideaPage);
        }
    }
}
