using Bizland.Entities;
using Bizland.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Prism.Ioc;
using MonkeyCache.FileStore;

namespace Bizland.Core.Helpers
{
    public class AutoUpdateHelper
    {
        readonly static string url = "https://api.github.com/repos/geofffranks/spruce/releases/latest";
        private static int size;
        public static async Task<bool> GetUpdate()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet &&
                   Barrel.Current.IsExpired(key: url))
            {
                TaskCompletionSource<bool> r = new TaskCompletionSource<bool>();
                string currentVersion = VersionTracking.CurrentVersion;
                var service = Prism.PrismApplicationBase.Current.Container.Resolve<IRequestProvider>();
                GitHubReleasesModel releasesModel = await service.GetAsync<GitHubReleasesModel>(url);
                if (releasesModel != null && !string.IsNullOrEmpty(releasesModel.tag_name))
                {
                    string lastReleasesVersion = releasesModel.tag_name;
                    string IgnoreVersion = Preferences.Get("IgnoreVersion", "");
                    if (lastReleasesVersion.Equals(IgnoreVersion))
                    {
                        r.SetResult(false);
                        return await r.Task;
                    }
                    string downloadurl = releasesModel.assets[0].browser_download_url;
                    size = releasesModel.assets[0].size;
                    bool hasupdate = ContainsVersion(currentVersion, lastReleasesVersion);
                    if (hasupdate && Device.RuntimePlatform.Equals(Device.Android))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            try
                            {
                                string content = string.Format("{0}\r\n\r\nSize({1}MB)", releasesModel.body, ((double)size / 1024 / 1024).ToString("f2"));
                                if (await App.Current.MainPage.DisplayAlert("New Version" + lastReleasesVersion, content, "Update", "Cancel"))
                                {

                                    DependencyService.Get<IDisplayMessage>().ShowMessageInfo("Update has been downloaded in the background, please wait");
                                    System.Net.WebClient webClient = new System.Net.WebClient();
                                    webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                                    webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;
                                    byte[] data = await webClient.DownloadDataTaskAsync(downloadurl);
                                    string fileName = releasesModel.assets[0].name;
                                    DependencyService.Get<IFileOpener>().OpenFile(data, fileName);
                                }
                                else
                                {
                                    Preferences.Set("IgnoreVersion", lastReleasesVersion);
                                }
                            }
                            catch (Exception ex)
                            {
                                DependencyService.Get<IDisplayMessage>().ShowMessageInfo("Update download failed:" + ex.Message);
                                DependencyService.Get<IProgressUpdate>().ProgressCancel(1);
                            }
                        });
                    }
                    else
                    {
                        r.SetResult(false);
                        return await r.Task;
                    }
                }
                r.SetResult(true);
                //Saves the cache and pass it a timespan for expiration
                Barrel.Current.Add(key: url, data: r.Task, expireIn: TimeSpan.FromDays(1));
                return await r.Task;
            }
            else
            {
                return Barrel.Current.Get<bool>(key: url);
            }
        }

        private static void WebClient_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            DependencyService.Get<IProgressUpdate>().ProgressCancel(1);
        }

        private static void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            int p = (int)(e.BytesReceived * 100 / size);
            DependencyService.Get<IProgressUpdate>().ProgressUpdate(1, p);
        }

        static bool ContainsVersion(string now, string releases)
        {
            string[] arr1 = now.Replace("v", "").Split('.');
            string[] arr2 = releases.Replace("v", "").Split('.');
            List<string> v1 = new List<string>();
            List<string> v2 = new List<string>();
            v1.AddRange(arr1);
            v2.AddRange(arr2);
            var arrLen = Math.Max(v1.Count, v2.Count);
            int l = Math.Abs(v1.Count - v2.Count);
            if (v1.Count < v2.Count)
                for (int i = 0; i < l; i++)
                    v1.Add("0");
            else if (v1.Count > v2.Count)
                for (int i = 0; i < l; i++)
                    v2.Add("0");
            if (v1.Count == 0 && v2.Count == 0)
            {
                return false;
            }
            else if (v1.Count == 0)
            {
                return true;
            }
            else if (v2.Count == 0)
            {
                return false;
            }
            for (var i = 0; i < arrLen; i++)
            {
                int result = Comp(int.Parse(v1[i]), int.Parse(v2[i]));
                if (result == 0)
                {
                    continue;
                }
                else
                {
                    if (result == -1)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        static int Comp(int n1, int n2)
        {
            if (n1 > n2)
            {
                return 1;
            }
            else if (n1 < n2)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
