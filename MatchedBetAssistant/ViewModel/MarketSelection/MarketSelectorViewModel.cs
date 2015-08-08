
using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class MarketSelectorViewModel : ViewModelBase
    {
        #region Private Fields
        private BetfairService service;
        private EventTypeCollection eventTypes;
        private ISelectableList currentSelection = null;
        private RelayCommand backCommand;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSelectorViewModel"/> class.
        /// </summary>
        /// <param name="service"></param>
        public MarketSelectorViewModel(BetfairService service)
        {
            this.service = service;

            var events = this.service.GetEventTypes();

            this.eventTypes = new EventTypeCollection(events.OrderBy(e => e.Name).ToList(), this.service);

            this.CurrentSelection = this.eventTypes;
        }


        public ISelectableList CurrentSelection
        {
            get 
            { 
                return this.currentSelection; 
            }
            set
            {
                if (this.currentSelection != null)
                {
                    this.currentSelection.MarketSelected -= OnMarketSelected;
                }

                this.currentSelection = value;
                if (this.currentSelection != null)
                {
                    this.currentSelection.MarketSelected += OnMarketSelected;
                }


                RaisePropertyChanged(() => CurrentSelection);
                RaisePropertyChanged(() => BreadcrumbString);
                BackCommand.RaiseCanExecuteChanged();
            }
        }

        public string BreadcrumbString
        {
            get { return this.CurrentSelection != null ? this.CurrentSelection.BreadcrumbString : string.Empty; }
        }

        public RelayCommand BackCommand
        {
            get
            {
                return this.backCommand ?? (this.backCommand = new RelayCommand(Back, CanGoBack));
            }
        }

        private void Back()
        {
            this.CurrentSelection = this.CurrentSelection.PreviousList;
        }

        private bool CanGoBack()
        {
            return this.CurrentSelection != null && this.CurrentSelection.PreviousList != null;
        }

        private void OnMarketSelected(object sender, MarketSelectedEventArgs args)
        {
            this.CurrentSelection = args.SelectedMarketList;
        }
    }
}
