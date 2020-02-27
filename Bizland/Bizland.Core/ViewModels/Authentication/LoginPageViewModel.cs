using Bizland.Core.Controls;
using Bizland.Core.Extensions;
using Bizland.Core.Helpers;
using Bizland.Core.Resource;
using Bizland.Entities;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bizland.Core.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        private bool rememberme;

        #endregion

        #region Constructor
        private readonly IDialogService dialogService;
        private readonly IFacebookClient _facebookService;
        private readonly IGoogleClientManager _googleService;
        /// <summary>
        /// Initializes a new instance for the <see cref="NoInternetConnectionPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigationService navigationService, IDialogService dialogService)
           : base(navigationService)
        {
            this._googleService = CrossGoogleClient.Current;
            this._facebookService = CrossFacebookClient.Current;
            this.dialogService = dialogService;
            this.LoginCommand = new Command(this.Login);
            this.PushForgotPasswordCommand = new Command(this.ForgotPassword);
            this.PushRegisterCommand = new Command(this.PushRegisterPage);
            this.LoginWithFaceBookCommand = new Command(this.LoginWithFacebook);
            this.LoginWithGoogleCommand = new Command(this.LoginWithGoogle);

            InitValidations();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the TryAgain button is clicked.
        /// </summary>
        public ICommand LoginCommand { get; set; }

        public ICommand LoginWithFaceBookCommand { get; set; }

        public ICommand LoginWithGoogleCommand { get; set; }

        public ICommand PushForgotPasswordCommand { get; set; }

        public ICommand PushRegisterCommand { get; set; }

        #endregion

        #region Properties
        public ValidatableObject<string> UserName { get; set; }

        public ValidatableObject<string> Password { get; set; }


        public bool Rememberme { get => rememberme; set => SetProperty(ref rememberme, value); }


        #endregion

        #region Methods
        private void InitValidations()
        {
            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            UserName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = MobileResource.Common_Property_NullOrEmpty("User name") });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = MobileResource.Common_Property_NullOrEmpty("Password") });
        }

        private bool Validate()
        {
            return UserName.Validate() && Password.Validate();
        }


        private void Login()
        {
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    //bool isValid = Validate();
                    //if (isValid)
                    //{

                    //    await NavigationService.NavigateAsync("/RootPage?selectedTab=HomePage");
                    //}
                    //else
                    //{
                    //    DisplayMessage.ShowMessageInfo("Login Error", 5000);
                    //}

                    var cam = await PermissionHelper.CheckCameraPermissions();
                    var photo = await PermissionHelper.CheckPhotoPermissions();
                    var storr = await PermissionHelper.CheckStoragePermissions();
                    if (cam && photo && storr)
                    {
                        //If we are on iOS, call GMMultiImagePicker.
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            //If the image is modified (drawings, etc) by the users, you will need to change the delivery mode to HighQualityFormat.
                            bool imageModifiedWithDrawings = false;
                            if (imageModifiedWithDrawings)
                            {
                                await GMMultiImagePicker.Current.PickMultiImage(true);
                            }
                            else
                            {
                                await GMMultiImagePicker.Current.PickMultiImage();
                            }

                            MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectediOS");
                            MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectediOS", (s, images) =>
                            {
                                //If we have selected images, put them into the carousel view.
                                if (images.Count > 0)
                                {

                                }
                            });
                        }
                        //If we are on Android, call IMediaService.
                        else if (Device.RuntimePlatform == Device.Android)
                        {
                            DependencyService.Get<IMediaService>().OpenGallery();

                            MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid");
                            MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid", (s, images) =>
                            {
                                //If we have selected images, put them into the carousel view.
                                if (images.Count > 0)
                                {

                                }
                            });
                        }
                    }
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }

        private void LoginWithFacebook()
        {
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    if (_facebookService.IsLoggedIn)
                    {
                        _facebookService.Logout();
                    }

                    EventHandler<FBEventArgs<string>> userDataDelegate = null;

                    userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                    {
                        switch (e.Status)
                        {
                            case FacebookActionStatus.Completed:
                                var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                                break;
                            case FacebookActionStatus.Canceled:
                                await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
                                break;
                            case FacebookActionStatus.Error:
                                await App.Current.MainPage.DisplayAlert("Facebook Auth", "Error", "Ok");
                                break;
                            case FacebookActionStatus.Unauthorized:
                                await App.Current.MainPage.DisplayAlert("Facebook Auth", "Unauthorized", "Ok");
                                break;
                        }

                        _facebookService.OnUserData -= userDataDelegate;
                    };

                    _facebookService.OnUserData += userDataDelegate;

                    string[] fbRequestFields = { "email", "first_name", "picture", "gender", "last_name" };
                    string[] fbPermisions = { "email" };
                    await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }

        private void LoginWithGoogle()
        {
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    if (!string.IsNullOrEmpty(_googleService.ActiveToken))
                    {
                        //Always require user authentication
                        _googleService.Logout();
                    }
                    _googleService.OnLogin += OnLoginGoogleCompleted;

                    try
                    {
                        await _googleService.LoginAsync();
                    }
                    catch (GoogleClientSignInNetworkErrorException e)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                    }
                    catch (GoogleClientSignInCanceledErrorException e)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                    }
                    catch (GoogleClientSignInInvalidAccountErrorException e)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                    }
                    catch (GoogleClientSignInInternalErrorException e)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                    }
                    catch (GoogleClientNotInitializedErrorException e)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                    }
                    catch (GoogleClientBaseException e)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                    }
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }


        private void OnLoginGoogleCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
        {
            if (loginEventArgs.Data != null)
            {
                GoogleUser googleUser = loginEventArgs.Data;
                var GivenName = googleUser.GivenName;
                var FamilyName = googleUser.FamilyName;
                var token = CrossGoogleClient.Current.ActiveToken;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
            }

            _googleService.OnLogin -= OnLoginGoogleCompleted;

        }


        void CloseDialogCallback(IDialogResult dialogResult)
        {

        }

        private void ForgotPassword()
        {
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    bool isValid = Validate();
                    if (isValid)
                    {
                        await NavigationService.NavigateAsync(new System.Uri("/NavigationPage/RootPage?selectedTab=MainPage", System.UriKind.Absolute));
                    }
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }
        private async void PushRegisterPage()
        {
            //SafeExecute(async () =>
            //{
            //    await NavigationService.NavigateAsync("RegisterPage", useModalNavigation: true);
            //});
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }

        #endregion
    }
}
