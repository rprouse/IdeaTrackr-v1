using System;
using System.Linq;

using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public partial class IdeaList : ContentPage
    {
        public IdeaList()
        {
            InitializeComponent();
        }

        public async void OnButtonClicked(object sender, EventArgs e)
        {
            var service = App.GetIdeaService();
            var ideas = await service.GetIdeas();
            message.Text = ideas.First().Name;
        }
    }
}
