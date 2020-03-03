using Bizland.Core.Constant;
using Bizland.Core.Extensions;
using Bizland.Core.Styles;
using Bizland.Utilities.Enums;
using MonkeyCache;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core
{
    public class ThemeServiceBase : IThemeService
    {
        private readonly IBarrel _barrel;

        public ThemeServiceBase()
        {
            Barrel.ApplicationId = "Bizland";
            _barrel = Barrel.Current;
        }

        public ThemeMode AppTheme
        {
            get => _barrel.Get<ThemeMode>(AppSettings.ThemeKey);
            set => _barrel.Add(AppSettings.ThemeKey, value, TimeSpan.MaxValue);
        }

        public ResourceDictionary CustomColors
        {
            get => _barrel.Get<ResourceDictionary>(AppSettings.CustomColorsKey) ?? new LightTheme();
            set => _barrel.Add(AppSettings.CustomColorsKey, value, TimeSpan.MaxValue);
        }

        public ThemeMode CurrentRuntimeTheme { get; private set; }

        public virtual void UpdateTheme(ThemeMode themeMode = ThemeMode.Auto)
        {
            switch (AppTheme)
            {
                case ThemeMode.Auto:
                    if (themeMode == ThemeMode.Dark)
                        goto case ThemeMode.Dark;
                    else
                        goto case ThemeMode.Light;
                case ThemeMode.Dark:
                    SetTheme(ThemeMode.Dark);
                    break;
                case ThemeMode.Light:
                    SetTheme(ThemeMode.Light);
                    break;
                case ThemeMode.Custom:
                    SetTheme(ThemeMode.Custom);
                    break;
                default:
                    break;
            }
        }

        private void SetTheme(ThemeMode themeMode)
        {
            if (CurrentRuntimeTheme == themeMode)
                return;

            SetColors(themeMode);

            CurrentRuntimeTheme = themeMode;
        }

        private void SetColors(ThemeMode themeMode)
        {
            var colors = themeMode.ToResourceDictionary(CustomColors);

            Application.Current.Resources.Clear();
            var style = new Bizland.Core.Styles.Styles();
            style.MergedDictionaries.Add(colors);

            Application.Current.Resources = style;
        }
    }
}
