using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class EventSelectorViewModel : ViewModelBase, ISelectableMarket
    {
        private Event modelEvent;

        public EventSelectorViewModel(Event modelEvent)
        {
            this.modelEvent = modelEvent;
        }

        public string Id { get { return this.modelEvent.Id; } }

        public string Name { get { return this.modelEvent.Name; } }
    }
}
