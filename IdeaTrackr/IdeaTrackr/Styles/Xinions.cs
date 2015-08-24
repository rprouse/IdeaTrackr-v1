using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IdeaTrackr.Styles
{
    /// <summary>
    /// Style extensions for Xamarin by Adam Wolf
    /// </summary>
    public static class Xinions
    {
        public static Style Extend(this Style _style)
        {
            var newStyle = new Style(_style.TargetType)
            {
                BasedOn = _style
            };
            return newStyle;
        }

        public static Style Set<T>(this Style _style, BindableProperty property, T value)
        {
            _style.Setters.Add(new Setter() { Property = property, Value = value });
            return _style;
        }

    }
}
