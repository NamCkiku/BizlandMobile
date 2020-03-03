using Bizland.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core
{
    public interface IThemeService
    {
        ResourceDictionary CustomColors { get; set; }
        ThemeMode AppTheme { get; set; }
        void UpdateTheme(ThemeMode themeMode = ThemeMode.Auto);
    }
}
