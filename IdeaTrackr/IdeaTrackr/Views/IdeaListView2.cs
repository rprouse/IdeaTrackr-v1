using IdeaTrackr.Models;
using IdeaTrackr.ViewModels;
using System.Diagnostics;

using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public class IdeaListView2 : ContentPage
    {
        IdeaListViewModel _viewModel;

        public IdeaListView2()
        {
            Title = "Idea";

            var listView = new ListView();
            listView.ItemTemplate = new DataTemplate(typeof(IdeaItemCell));
            listView.ItemSelected += (sender, e) => {
                var idea = (Idea)e.SelectedItem;
                var ideaPage = new IdeaView(idea);

                ((App)App.Current).ResumeAtIdeaId = idea.Id;
                Debug.WriteLine("setting ResumeAtIdeaId = " + idea.Id);

                Navigation.PushAsync(ideaPage);
            };
            listView.SetBinding(ListView.ItemsSourceProperty, new Binding("Ideas"));

            var layout = new StackLayout();
            if (Device.OS == TargetPlatform.WinPhone)
            { // WinPhone doesn't have the title showing
                layout.Children.Add(new Label
                {
                    Text = "Idea",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                });
            }
            layout.Children.Add(listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            Content = layout;

            #region toolbar
            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, AddIdea, 0, 0);
            }
            else if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", AddIdea, 0, 0);
            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                tbi = new ToolbarItem("Add", "add.png", AddIdea, 0, 0);
            }

            ToolbarItems.Add(tbi);
            #endregion

            _viewModel = new IdeaListViewModel(Navigation);
            BindingContext = _viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtIdeaId = "";
            await _viewModel.LoadAsync();
        }

        void AddIdea()
        {
            var idea = new Idea();
            var ideaPage = new IdeaView(idea);
            Navigation.PushAsync(ideaPage);
        }
    }
}
