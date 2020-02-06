using System;
using Xamarin.Forms;

namespace Bizland.Core.Views
{
    public partial class RootPage : TabbedPage
    {
        public RootPage()
        {
            InitializeComponent();
            this.Children.Add(new HomePage()
            {
                Title = "Search",
                IconImageSource = "ic_search.png"
            });
            this.Children.Add(new LoginPage()
            {
                Title = "Catalog",
                IconImageSource = "ic_catalog.png"
            });
            this.Children.Add(new RegisterPage()
            {
                Title = "Bookmarks",
                IconImageSource = "ic_bookmark.png"
            });
            this.Children.Add(new OTPPage()
            {
                Title = "Profile",
                IconImageSource = "ic_profile.png"
            });
        }
    }
}
