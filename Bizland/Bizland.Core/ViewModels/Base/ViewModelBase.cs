using Bizland.Core.Helpers;

using Prism;
using Prism.AppModel;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bizland.Core.ViewModels
{
    public abstract class ViewModelBase : ExtendedBindableObject, INavigationAware, IInitialize, IInitializeAsync, IDestructible, IApplicationLifecycleAware, IDisposable
    {
        protected INavigationService NavigationService { get; private set; }
        protected IDisplayMessage DisplayMessage { get; private set; }

        private string title;
        public virtual string Title { get => title; set => SetProperty(ref title, value); }

        private bool isBusy = false;
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        private bool hasData = true;
        public bool HasData { get => hasData; set => SetProperty(ref hasData, value); }

        public bool isNotFound;
        public bool IsNotFound { get => isNotFound; set => SetProperty(ref isNotFound, value); }

        public int[] _vehicleGroups;
        public int[] VehicleGroups { get => _vehicleGroups; set => SetProperty(ref _vehicleGroups, value); }

        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            DisplayMessage = PrismApplicationBase.Current.Container.Resolve<IDisplayMessage>();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        ~ViewModelBase()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsConnected));

            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                // await NavigationService.NavigateAsync("NetworkPage");
                //await PopupNavigation.Instance.PushAsync(new NetworkPage());
            }
            else
            {
                //await NavigationService.GoBackAsync();
                //if (PopupNavigation.Instance.PopupStack.Count > 0)
                //{
                //    await PopupNavigation.Instance.PopAllAsync();
                //}
            }
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        private bool viewHasAppeared;
        public bool ViewHasAppeared { get => viewHasAppeared; set => SetProperty(ref viewHasAppeared, value); }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!ViewHasAppeared)
            {
                OnPageAppearingFirstTime();

                ViewHasAppeared = true;
            }
        }

        public virtual void OnPageAppearingFirstTime()
        {
        }

        public virtual void OnPushed()
        {
        }

        public virtual void OnSleep()
        {
        }

        public virtual void OnResume()
        {
        }

        public virtual void Dispose()
        {
        }

        protected void SafeExecute(Action action)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async void SafeExecute(Func<Task> func)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await func?.Invoke();
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected void TryExecute(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected async void TryExecute(Func<Task> func)
        {
            try
            {
                await func?.Invoke();
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected Task RunOnBackground(Action action, Action onComplete = null, Action<Exception> onError = null, Action finalAction = null, CancellationTokenSource cts = null, bool showLoading = false)
        {
            //if (showLoading)
            //    UserDialogs.Instance.ShowLoading();

            return (cts != null ? Task.Run(action, cts.Token) : Task.Run(action)).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
            {
                if (task.Status == TaskStatus.RanToCompletion && !task.IsCanceled && (cts == null ? true : !cts.IsCancellationRequested))
                {
                    onComplete?.Invoke();
                }
                else if (task.IsFaulted)
                {
                    LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, task.Exception);
                    onError?.Invoke(task.Exception);
                }

                //if (showLoading)
                //    UserDialogs.Instance.HideLoading();

                finalAction?.Invoke();
            }));
        }

        protected Task RunOnBackground<T>(Func<Task<T>> action, Action<T> onComplete = null, Action<Exception> onError = null, Action finalAction = null, CancellationTokenSource cts = null, bool showLoading = false)
        {
            //if (showLoading)
            //    UserDialogs.Instance.ShowLoading();

            return (cts != null ? Task.Run(action, cts.Token) : Task.Run(action)).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
            {
                if (task.Status == TaskStatus.RanToCompletion && !task.IsCanceled && (cts == null ? true : !cts.IsCancellationRequested))
                {
                    onComplete?.Invoke(task.Result);
                }
                else if (task.IsFaulted)
                {
                    LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, task.Exception);
                    onError?.Invoke(task.Exception);
                }

                //if (showLoading)
                //    UserDialogs.Instance.HideLoading();

                finalAction?.Invoke();
            }));
        }

        public ICommand ClosePageCommand
        {
            get
            {
                return new Command(() =>
                {
                    SafeExecute(async () =>
                    {
                        await NavigationService.GoBackAsync(useModalNavigation: true);
                    });
                });
            }
        }

        protected TControl GetControl<TControl>(string control)
        {
            return PageUtilities.GetCurrentPage(Application.Current.MainPage).FindByName<TControl>(control);
        }

        public void SetFocus(string control)
        {
            TryExecute(() => PageUtilities.GetCurrentPage(Application.Current.MainPage).FindByName<VisualElement>(control)?.Focus());
        }

        public void GoBack(bool isModal)
        {
            TryExecute(async () => await NavigationService.GoBackAsync(useModalNavigation: isModal));
        }
    }
}