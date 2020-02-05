using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core.Extensions
{
    public static class PageExtension
    {
        public static TControl GetControl<TControl>(this Page page, string control)
        {
            return page.FindByName<TControl>(control);
        }

        public static void SetFocus(this Page page, string control)
        {
            page.FindByName<VisualElement>(control)?.Focus();
        }

        public static void RemovePage(this INavigationService navigationService, string name)
        {
            var formsNav = ((Prism.Common.IPageAware)navigationService).Page.Navigation;
            var pageType = PageNavigationRegistry.GetPageType(name);
            var page = formsNav.NavigationStack.LastOrDefault(p => p.GetType() == pageType) ?? formsNav.ModalStack.LastOrDefault(p => p.GetType() == pageType);
            formsNav.RemovePage(page);
        }

        public static async Task SafeNavigateAsync(this INavigationService navigationService, string name, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            var formsNav = ((Prism.Common.IPageAware)navigationService).Page.Navigation;
            IReadOnlyCollection<Page> stack = formsNav.NavigationStack;
            if (useModalNavigation != null && useModalNavigation.Value)
            {
                stack = formsNav.ModalStack;
            }
            var pageType = PageNavigationRegistry.GetPageType(name);
            if (stack.LastOrDefault()?.GetType() != pageType)
            {
                await navigationService.NavigateAsync(name, parameters, useModalNavigation, animated);
            }
        }
    }
}