using Bizland.Core.Styles;
using Bizland.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core.Extensions
{
    public static class ThemeExtensions
    {
        public static ResourceDictionary ToResourceDictionary(this ThemeMode mode, ResourceDictionary customColors = default)
        {
            switch (mode)
            {
                case ThemeMode.Dark:
                    return new DarkTheme();
                case ThemeMode.Light:
                    return new LightTheme();
                default:
                    return customColors;
            }
        }
    }
}
