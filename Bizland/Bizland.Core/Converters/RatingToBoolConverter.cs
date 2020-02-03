﻿using System;

using Xamarin.Forms;

namespace Bizland.Core
{
    public class RatingToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rating = (int)value;
            if (rating == 1)
                return true;
            if (rating == 2)
                return true;
            if (rating == 3)
                return true;
            if (rating == 4)
                return false;
            if (rating == 5)
                return false;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}