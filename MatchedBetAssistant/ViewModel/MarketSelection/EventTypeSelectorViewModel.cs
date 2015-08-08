using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class EventTypeSelectorViewModel : ViewModelBase, ISelectableMarket
    {
        private EventType eventType;

        internal EventTypeSelectorViewModel(EventType eventType)
        {
            this.eventType = eventType;
        }

        public string Id { get { return this.eventType.Id; } }

        public string Name { get { return this.eventType.Name; } }

    }
}
