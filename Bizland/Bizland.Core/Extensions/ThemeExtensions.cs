using Bizland.Core.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core.Extensions
{
    public static class ThemeExtensions
    {
        public static void ApplyDarkTheme(this ResourceDictionary resources)
        {
            var mergedDictionaries = resources.MergedDictionaries;
            var lightTheme = mergedDictionaries.OfType<LightTheme>().FirstOrDefault();
            if (lightTheme != null)
            {
                mergedDictionaries.Remove(lightTheme);
            }
            mergedDictionaries.Add(new DarkTheme());
        }

        public static void ApplyLightTheme(this ResourceDictionary resources)
        {
            var mergedDictionaries = resources.MergedDictionaries;
            var darkTheme = mergedDictionaries.OfType<DarkTheme>().FirstOrDefault();
            if (darkTheme != null)
            {
                mergedDictionaries.Remove(darkTheme);
            }

            mergedDictionaries.Add(new LightTheme());
        }
    }
}
