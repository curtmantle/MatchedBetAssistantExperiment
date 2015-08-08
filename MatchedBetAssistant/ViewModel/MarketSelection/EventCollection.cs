using System;
using System.Collections.Generic;
using System.Linq;
using MatchedBetAssistant.Model;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class EventCollection : SelectorCollection<EventSelectorViewModel>
    {
        public EventCollection() { }

        public EventCollection(IEnumerable<EventSelectorViewModel> events, ISelectableList previous, BetfairService service)
            : base(events, previous, service)
        {

        }

        public EventCollection(IEnumerable<Event> events, ISelectableList previous, BetfairService service)
            : base(previous, service)
        {
            this.AddRange(events.Select(x => new EventSelectorViewModel(x)).ToList());
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
                
                var result = this.Service.GetMarketCatalogueForEvent(this.SelectedItem.Id);


            }
        }

        
    }
}
