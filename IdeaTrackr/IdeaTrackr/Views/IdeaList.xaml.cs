using IdeaTrackr.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var client = new MobileServiceClient("http://localhost:60978/");
            var ideas = await client.GetTable<Idea>().ReadAsync();
            message.Text = ideas.First().Name;
        }
    }
}
