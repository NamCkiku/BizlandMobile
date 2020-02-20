using Bizland.Core;
using Bizland.Core.Constant;
using Bizland.Styles;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using System;

namespace Bizland
{
    public class BizlandApp : App
    {
        public BizlandApp(IPlatformInitializer initializer) : base(initializer)
        {
        }

        public override string OneSignalKey => base.OneSignalKey;

        protected async override void OnInitialized()
        {
            Resources.MergedDictionaries.Add(new Colors());

            base.OnInitialized();

            AppCenter.Start("ios=93e14f47-aa36-4b9d-8cf6-1ea068ff4235;" +
                 "android=c1cfc93a-c3b6-4736-83d8-244f05a198f6",
                 typeof(Analytics), typeof(Crashes));

            //MainPage = new AppShell();
            //Nếu cài app lần đầu tiên hoặc có sự thay đổi dữ liệu trên server thì sẽ vào trang cập nhật thông tin vào localDB
            //await NavigationService.NavigateAsync("/OnBoardingPage");

            await NavigationService.NavigateAsync("/RootPage?selectedTab=HomePage");
        }

        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            var option = uri.ToString().Replace(ParameterKey.AppShortcutUriBase, "");
            if (!string.IsNullOrEmpty(option))
            {
                switch (option)
                {
                    case "DETAIL1":
                        await NavigationService.NavigateAsync("/RootPage?selectedTab=HomePage");
                        break;
                    case "DETAIL2":

                        await NavigationService.NavigateAsync("/OnBoardingPage");
                        break;
                }
            }
            else
                base.OnAppLinkRequestReceived(uri);
        }
    }
}