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
            _viewModel = new IdeaListViewModel(Navigation);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadAsync();
        }

        public void OnIdeaTapped(object sender, ItemTappedEventArgs e)
        {
            // Display detail page
            var idea = e.Item as Idea;
            Navigation.PushAsync(new IdeaView(idea));
        }
    }
}
