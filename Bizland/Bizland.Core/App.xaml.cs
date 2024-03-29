﻿using Bizland.Core.Helpers;
using Bizland.Core.Setup;
using Bizland.Core.Styles;
using Bizland.Utilities.Constant;

using Prism;
using Prism.Events;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Bizland.Core
{
    public partial class App
    {
        /*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public App() : this(null)
        {
        }

        private readonly IEventAggregator _eventAggregator;

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            _eventAggregator = Current.Container.Resolve<IEventAggregator>();

        }

        public virtual string OneSignalKey => Config.OneSignalKey;

        protected override IContainerExtension CreateContainerExtension() => PrismContainerExtension.Current;

        protected override void OnInitialized()
        {

            ShortcutsHelper.AddShortcuts();

            InitializeComponent();

            BizlandSetup.Initialize();

            OneSignalHelper.RegisterOneSignal(OneSignalKey);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            BizlandSetup.RegisterServices(containerRegistry);
            BizlandSetup.RegisterPages(containerRegistry);
            var _themeService = Current.Container.Resolve<IThemeService>();
            _themeService.UpdateTheme();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _eventAggregator.GetEvent<OnResumeEvent>().Publish(true);
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            _eventAggregator.GetEvent<OnSleepEvent>().Publish(true);
        }
    }
}