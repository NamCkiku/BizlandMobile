using System;
using System.Globalization;

using Xamarin.Forms;

namespace Bizland.Core
{
    public class AddRootImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "ic_launcher.png";
            }

            if (string.IsNullOrEmpty(value.ToString()))
                return "ic_launcher.png";
            else
                if (!value.ToString().Contains("/"))
            {
                return value.ToString();
            }
            else
            {
                return $"{""}{value.ToString()}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}