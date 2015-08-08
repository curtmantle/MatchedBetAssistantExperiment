using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class EventTypeCollection : SelectorCollection<EventTypeSelectorViewModel>
    {
        private BetfairService service;

        public EventTypeCollection(IEnumerable<EventType> events, BetfairService service)
        {
            this.service = service;
            this.AddRange(events.Select(x => new EventTypeSelectorViewModel(x)).ToList());
        }

        public EventTypeCollection(IEnumerable<EventTypeSelectorViewModel> events, BetfairService service)
            : base(events)
        {
            this.service = service;
        }

        public override INamed SelectedItem
        {
            get
            {
                return base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;

                //when we change the selected item - bring back a list of countries
                var list = CreateCountryList();
                this.BreadcrumbSuffix = base.SelectedItem.Name;

                OnMarketSelected(this, new MarketSelectedEventArgs(list));
            }
        }

        private CountryCollection CreateCountryList()
        {
            CountryCollection countries;
            var selectedItem = this.SelectedItem as EventTypeSelectorViewModel;
            if (this.SelectedItem != null)
            {
                countries = new CountryCollection(service.GetCountriesForEventType(selectedItem.Id), this);
            }
            else
            {
                countries = new CountryCollection();
            }

            return countries;
        }
    }
}
