using IdeaTrackr.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace IdeaTrackr.Views
{
    public class IdeaItemCell : ViewCell
    {
        public IdeaItemCell()
        {
            var ideaLabel = new Label
            {
                TextColor = StyleKit.PrimaryTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            ideaLabel.SetBinding(Label.TextProperty, new Binding("Name"));

            var ratingLabel = new Label
            {
                TextColor = StyleKit.SecondaryTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            ideaLabel.SetBinding(Label.TextProperty, new Binding("Rating", BindingMode.OneWay, null, null, "Rating: {0}"));

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { ideaLabel, ratingLabel }
            };

            View = layout;
        }

        protected override void OnBindingContextChanged()
        {
            // Fixme : this is happening because the View.Parent is getting
            // set after the Cell gets the binding context set on it. Then it is inheriting
            // the parents binding context.
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}
