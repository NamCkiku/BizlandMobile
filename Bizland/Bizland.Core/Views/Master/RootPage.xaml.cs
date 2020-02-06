using System;
using Xamarin.Forms;

namespace Bizland.Core.Views
{
    public partial class RootPage : FormsCurvedBottomNavigation.CurvedBottomTabbedPage
    {
        public RootPage()
        {
            InitializeComponent();
            this.Children.Add(new HomePage()
            {
                Title = "Giám sát",
                IconImageSource = "ic_home.png"
            });
            this.Children.Add(new LoginPage()
            {
                Title = "Lộ trình",
                IconImageSource = "ic_home.png"
            });
            this.Children.Add(new RegisterPage()
            {
                Title = "Báo cáo",
                IconImageSource = "ic_home.png"
            });
            this.Children.Add(new OTPPage()
            {
                Title = "Camera",
                IconImageSource = "ic_home.png"
            });
        }

        private void CurvedBottomTabbedPage_FabIconClicked(object sender, EventArgs e)
        {
            //Do something here
        }
    }
}
