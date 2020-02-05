using Bizland.Core.Events;
using Bizland.Core.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bizland.Core.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private CancellationTokenSource cts;
        public ICommand SearchCountryCommmand { get; set; }
        public ICommand SelectedCommand { get; set; }

        public CountryPageViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator)
           : base(navigationService)
        {
            this._eventAggregator = eventAggregator;

            SearchCountryCommmand = new Command<TextChangedEventArgs>(SearchCountryWithText);
            SelectedCommand = new Command<CountryCode>(SelectedContry);

            Title = "Country";
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            GetAllCountry();
        }
        private List<CountryCode> _countryCollection;

        public List<CountryCode> CountryCollection
        {
            get => _countryCollection; set => SetProperty(ref _countryCollection, value);
        }

        private ObservableCollection<Grouping<string, CountryCode>> _countryCollectionSearched;

        public ObservableCollection<Grouping<string, CountryCode>> CountryCollectionSearched
        {
            get => _countryCollectionSearched; set => SetProperty(ref _countryCollectionSearched, value);
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText; set => SetProperty(ref _searchText, value);
        }

        private void GetAllCountry()
        {
            TryExecute(() =>
            {
                var lst = CountryCode.All;
                if (lst != null && lst.Count > 0)
                {
                    var groupedData =
                          lst.GroupBy(p => p.NameSort)
                              .Select(p => new Grouping<string, CountryCode>(p))
                              .ToList();
                    CountryCollection = lst;
                    CountryCollectionSearched = new ObservableCollection<Grouping<string, CountryCode>>(groupedData);
                }
            });

        }

        private void SearchCountryWithText(TextChangedEventArgs args)
        {
            if (CountryCollection == null || CountryCollection.Count == 0)
                return;

            if (cts != null)
                cts.Cancel(true);

            cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                await Task.Delay(500, cts.Token);

                if (string.IsNullOrWhiteSpace(args.NewTextValue))
                    return CountryCollection;
                return CountryCollection.FindAll(v => v.Name.ToUpper().Contains(args.NewTextValue.ToUpper()) || v.DialCode.ToUpper().Contains(args.NewTextValue.ToUpper()));
            }, cts.Token).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    var groupedData =
                                      task.Result.GroupBy(p => p.NameSort)
                                          .Select(p => new Grouping<string, CountryCode>(p))
                                          .ToList();

                    CountryCollectionSearched = new ObservableCollection<Grouping<string, CountryCode>>(groupedData);
                }
            }));
        }

        private void SelectedContry(CountryCode args)
        {
            if (args != null)
            {
                TryExecute(async () =>
                {

                    _eventAggregator.GetEvent<SelectCountryEvent>().Publish(args);
                    await NavigationService.GoBackAsync(useModalNavigation: true);
                });
            }
        }
    }
}
