using System;
using System.Globalization;

using Xamarin.Forms;

namespace Bizland.Core
{
    public class MinutesOfDrivingTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Format("{0} {1}", 0, "phút");
            }
            string stringValue = value.ToString();
            return string.Format("{0} {1}", stringValue, "phút");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}