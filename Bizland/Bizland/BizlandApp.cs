using Bizland.Core;
using Bizland.Styles;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;

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


            //Nếu cài app lần đầu tiên hoặc có sự thay đổi dữ liệu trên server thì sẽ vào trang cập nhật thông tin vào localDB
            await NavigationService.NavigateAsync("/NoPhotosPage");
        }
    }
}