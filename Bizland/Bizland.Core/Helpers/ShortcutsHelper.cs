using Bizland.Core.Constant;
using Plugin.AppShortcuts;
using Plugin.AppShortcuts.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bizland.Core.Helpers
{
    public static class ShortcutsHelper
    {
        public static async void AddShortcuts()
        {
            if (CrossAppShortcuts.IsSupported)
            {

                var shortCurts = await CrossAppShortcuts.Current.GetShortcuts();
                if (shortCurts.FirstOrDefault(prop => prop.Label == "Detail") == null)
                {
                    var shortcut = new Shortcut()
                    {
                        Label = "Detail",
                        Description = "Go to Detail",
                        Icon = new ContactIcon(),
                        Uri = $"{ParameterKey.AppShortcutUriBase}{"DETAIL1"}"
                    };
                    await CrossAppShortcuts.Current.AddShortcut(shortcut);
                }

                if (shortCurts.FirstOrDefault(prop => prop.Label == "Detail 2") == null)
                {
                    var shortcut = new Shortcut()
                    {
                        Label = "Detail 2",
                        Description = "Go to Detail 2",
                        Icon = new UpdateIcon(),
                        Uri = $"{ParameterKey.AppShortcutUriBase}{"DETAIL2"}"
                    };
                    await CrossAppShortcuts.Current.AddShortcut(shortcut);
                }

            }

        }
    }
}
