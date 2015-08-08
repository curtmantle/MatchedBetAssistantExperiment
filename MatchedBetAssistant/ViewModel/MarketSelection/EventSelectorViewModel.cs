
using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{

    public class EventSelectorViewModel : ViewModelBase
    {
        #region Private Fields
        private EventType selectedEventType;
        private BetfairService service;
        private IList<EventType> eventTypes;
        private IList<Country> countriesForEventType;
        private int selectedIndex;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSelectorViewModel"/> class.
        /// </summary>
        /// <param name="service"></param>
        public EventSelectorViewModel(BetfairService service)
        {
            this.service = service;

            var events = this.service.GetEventTypes();

            this.eventTypes = events.OrderBy(e => e.Name).ToList();
        }

        public IList<EventType> EventTypes
        {
            get { return this.eventTypes; }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                if (this.selectedIndex == value)
                    return;
                
                this.selectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);
            }
        }

        public EventType SelectedEventType
        {
            get { return this.selectedEventType; }
            set
            {
                if (this.selectedEventType == value)
                    return;
                this.selectedEventType = value;

                CreateCountryList();

                RaisePropertyChanged(() => SelectedEventType);
                RaisePropertyChanged(() => SelectedEventName);
            }
        }

        public string SelectedEventName
        {
            get
            {
                return this.selectedEventType != null ? this.selectedEventType.Name : string.Empty;
            }
        }

        public IList<Country> EventTypeCountries
        {
            get
            {
                return this.countriesForEventType;
            }
            set
            {
                this.countriesForEventType = value;

                RaisePropertyChanged(() => EventTypeCountries);
            }
        }

        private void CreateCountryList()
        {
            if (this.SelectedEventType != null)
            {
                this.EventTypeCountries = service.GetCountriesForEventType(this.selectedEventType.Id);
            } 
            else
            {
                this.EventTypeCountries = new List<Country>();
            }


        }
    }
}
