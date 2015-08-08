using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class CountryCollection : SelectorCollection<CountrySelectorViewModel>
    {
        public CountryCollection() { }

        public CountryCollection(IEnumerable<CountrySelectorViewModel> countries, ISelectableList previous, BetfairService service)
            : base(countries, previous, service)
        {

        }

        public CountryCollection(IEnumerable<Country> countries, ISelectableList previous, BetfairService service)
            : base(previous, service)
        {
            this.AddRange(countries.Select(x => new CountrySelectorViewModel(x)).ToList());
            
        }

        public override ISelectableMarket SelectedItem
        {
            get
            {
                return base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;

                this.BreadcrumbSuffix = base.SelectedItem.Name;

                var eventTypeId = this.PreviousList.SelectedItem.Id;
                ISelectableList result;
                if (eventTypeId == "1")
                {
                    result = CreateCompetitionList();
                }
                else
                {
                    result = CreateEventList();
                }

                OnMarketSelected(this, new MarketSelectedEventArgs(result));

            }
        }

        private EventCollection CreateEventList()
        {
            EventCollection countries;
            var selectedItem = this.SelectedItem;
            var previousId = this.PreviousList.SelectedItem.Id;
            if (this.SelectedItem != null)
            {
                countries = new EventCollection(Service.GetEventsForEventTypeWithinCountry(previousId, SelectedItem.Id), this, Service);
            }
            else
            {
                countries = new EventCollection();
            }

            return countries;
        }

        private CompetitionCollection CreateCompetitionList()
        {
            CompetitionCollection countries;
            var previousId = this.PreviousList.SelectedItem.Id;
            if (this.SelectedItem != null)
            {
                countries = new CompetitionCollection(Service.GetCompetitionsForEventTypeWithinCountry(previousId, SelectedItem.Id), this, Service);
            }
            else
            {
                countries = new CompetitionCollection();
            }

            return countries;
        }
       
    }
}
