using System;
using System.Globalization;

using Xamarin.Forms;

namespace Bizland.Core
{
    public class KmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            float val = float.Parse(value.ToString());

            if (val == 0)
                return $"0 km";
            else
                return $"{Math.Round(val)} km";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}