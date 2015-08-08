using System;
using System.Collections.Generic;
using System.Linq;
using MatchedBetAssistant.Model;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{


    public abstract class SelectorCollection<T> : List<T>, ISelectableList where T : ISelectableMarket
    {
        private BetfairService service;
        private ISelectableMarket selectedItem;
        private ISelectableList previousList;

        protected SelectorCollection()
        { }

        protected SelectorCollection(IEnumerable<T> collection, BetfairService service)
            : base(collection)
        {
            this.service = service;
            this.previousList = null;
        }

        protected SelectorCollection(IEnumerable<T> collection, ISelectableList previousList, BetfairService service)
            : base(collection)
        {
            this.service = service;
            this.previousList = previousList;
        }

        protected SelectorCollection(ISelectableList list, ISelectableList previousList, BetfairService service)
        {
            this.service = service;
            this.previousList = previousList;
            var newList = list as SelectorCollection<T>;
            this.AddRange(newList);
        }

        protected SelectorCollection(ISelectableList previousList, BetfairService service)
        {
            this.previousList = previousList;
            this.service = service;
        }

        public virtual ISelectableMarket SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
            }
        }

        public ISelectableList PreviousList
        {
            get { return this.previousList; }
            protected set
            {
                this.previousList = value;
            }
        }

        public virtual string BreadcrumbString
        {
            get { return previousList != null ? this.previousList.BreadcrumbString + "/" + BreadcrumbSuffix : this.BreadcrumbSuffix; }
        }

        protected string BreadcrumbSuffix { get; set; }

        protected BetfairService Service
        {
            get { return this.service; }
        }

        public event MarketSelectedEventHandler MarketSelected;

        protected virtual void OnMarketSelected(object sender, MarketSelectedEventArgs args)
        {
            MarketSelectedEventHandler handler = MarketSelected;
            if (handler != null)
                handler(sender, args);
        }

    }
}
