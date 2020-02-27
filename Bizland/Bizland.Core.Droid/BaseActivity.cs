using Android.App;
using Android.Content;
using Android.Database;
using Android.Provider;
using Android.Runtime;

using Bizland.Core.Droid.DependencyServices;

using Plugin.FacebookClient;
using Plugin.GoogleClient;

using Prism;
using Prism.Ioc;

using Shiny;

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Bizland.Core.Droid
{
    public class BaseActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            AndroidShinyHost.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        //{
        //    base.OnActivityResult(requestCode, resultCode, intent);
        //    FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
        //    GoogleClientManager.OnAuthCompleted(requestCode, resultCode, intent);
        //}

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
                containerRegistry.RegisterInstance<IDisplayMessage>(new DisplayMessageService());
                containerRegistry.RegisterInstance<ISettingsService>(new SettingsService());
                containerRegistry.RegisterInstance<ITooltipService>(new DroidTooltipService());
                containerRegistry.RegisterInstance<IAudioManager>(new DroidAudioManager());
                containerRegistry.RegisterInstance<IStatusBarStyleManager>(new StatusBarStyleManager());
            }
        }

        #region Image Picker Implementation

        public static int OPENGALLERYCODE = 100;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, intent);

            //If we are calling multiple image selection, enter into here and return photos and their filepaths.
            if (requestCode == OPENGALLERYCODE && resultCode == Result.Ok)
            {
                List<string> images = new List<string>();

                if (intent != null)
                {
                    //Separate all photos and get the path from them all individually.
                    ClipData clipData = intent.ClipData;
                    if (clipData != null)
                    {
                        for (int i = 0; i < clipData.ItemCount; i++)
                        {
                            ClipData.Item item = clipData.GetItemAt(i);
                            Android.Net.Uri uri = item.Uri;
                            var path = GetRealPathFromURI(uri);

                            if (path != null)
                            {
                                images.Add(path);
                            }
                        }
                    }
                    else
                    {
                        Android.Net.Uri uri = intent.Data;
                        var path = GetRealPathFromURI(uri);

                        if (path != null)
                        {
                            images.Add(path);
                        }
                    }

                    //Send our images to the carousel view.
                    MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid", images);
                }
            }
        }

        /// <summary>
        ///     Get the real path for the current image passed.
        /// </summary>
        public String GetRealPathFromURI(Android.Net.Uri contentURI)
        {
            try
            {
                ICursor imageCursor = null;
                string fullPathToImage = "";

                imageCursor = ContentResolver.Query(contentURI, null, null, null, null);
                imageCursor.MoveToFirst();
                int idx = imageCursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);

                if (idx != -1)
                {
                    fullPathToImage = imageCursor.GetString(idx);
                }
                else
                {
                    ICursor cursor = null;
                    var docID = DocumentsContract.GetDocumentId(contentURI);
                    var id = docID.Split(':')[1];
                    var whereSelect = MediaStore.Images.ImageColumns.Id + "=?";
                    var projections = new string[] { MediaStore.Images.ImageColumns.Data };

                    cursor = ContentResolver.Query(MediaStore.Images.Media.InternalContentUri, projections, whereSelect, new string[] { id }, null);
                    if (cursor.Count == 0)
                    {
                        cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projections, whereSelect, new string[] { id }, null);
                    }
                    var colData = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Data);
                    cursor.MoveToFirst();
                    fullPathToImage = cursor.GetString(colData);
                }
                return fullPathToImage;
            }
            catch (Exception)
            {
            }
            return null;
        }

        #endregion Image Picker Implementation
    }
}