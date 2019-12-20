using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePongMobile.MarkUpExtentions
{
    [ContentProperty("ID")]
    class EmbeddedImages : IMarkupExtension
    {
        public string ID { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(ID))
                return null;
            return ImageSource.FromResource(ID);

        }
    }
}
