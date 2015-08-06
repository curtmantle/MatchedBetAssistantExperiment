using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.Model.Betfair.Account
{
    public class AccountDetails
    {
        public string CurrencyCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LocaleCode { get; set; }

        public string Region { get; set; }

        public string TimeZone { get; set; }

        public string DiscountRate { get; set; }

        public int PointsBalance { get; set; }

        public string CountryCode { get; set; }
    }
}