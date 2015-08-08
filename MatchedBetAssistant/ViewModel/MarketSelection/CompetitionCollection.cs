using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class CompetitionCollection : SelectorCollection<CompetitionSelectorViewModel>
    {
        public CompetitionCollection() { }

        public CompetitionCollection(IEnumerable<CompetitionSelectorViewModel> countries, ISelectableList previous, BetfairService service)
            : base(countries, previous, service)
        {

        }

        public CompetitionCollection(IEnumerable<Competition> countries, ISelectableList previous, BetfairService service)
            : base(previous, service)
        {
            this.AddRange(countries.Select(x => new CompetitionSelectorViewModel(x)).ToList());

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

                var result = CreateEventList();

                OnMarketSelected(this, new MarketSelectedEventArgs(result));

            }
        }

        private EventCollection CreateEventList()
        {
            EventCollection countries;
            if (this.SelectedItem != null)
            {
                countries = new EventCollection(Service.GetEventsForCompetition(SelectedItem.Id), this, Service);
            }
            else
            {
                countries = new EventCollection();
            }

            return countries;
        }
    }
}
