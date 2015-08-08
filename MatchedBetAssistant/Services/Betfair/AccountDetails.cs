using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchedBetAssistant.Services.Betfair
{
    public class AccountDetails
    {
        [JsonProperty(PropertyName = "currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "localeCode")]
        public string LocaleCode { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty(PropertyName = "discountRate")]
        public string DiscountRate { get; set; }

        [JsonProperty(PropertyName = "pointsBalance")]
        public int PointsBalance { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }
    }
}