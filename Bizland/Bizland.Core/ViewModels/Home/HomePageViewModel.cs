using Bizland.Core.Delegates;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Shiny.Locations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Bizland.Core.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IGpsListener _gpsListener;
        private readonly IGpsManager _gpsManager;

        public HomePageViewModel(INavigationService navigationService, IGpsManager gpsManager, IGpsListener gpsListener) : base(navigationService)
        {
            _gpsManager = gpsManager;
            _gpsListener = gpsListener;
            _gpsListener.OnReadingReceived += OnReadingReceived;
            StartShinyLocation();
        }

        public string LocationMessage { get; set; }
        void OnReadingReceived(object sender, GpsReadingEventArgs e)
        {
            LocationMessage = $"{e.Reading.Position.Latitude}, {e.Reading.Position.Longitude}";
            Debug.WriteLine("OnReadingReceived: " + LocationMessage);
        }


        private void StartShinyLocation()
        {
            SafeExecute(async () =>
            {

                if (_gpsManager.IsListening)
                {
                    await _gpsManager.StopListener();
                }

                await _gpsManager.StartListener(new GpsRequest
                {
                    UseBackground = true,
                    Priority = GpsPriority.Highest,
                    Interval = TimeSpan.FromSeconds(5),
                    ThrottledInterval = TimeSpan.FromSeconds(3) //Should be lower than Interval
                });

            });

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _gpsListener.OnReadingReceived -= OnReadingReceived;
        }
    }
}
