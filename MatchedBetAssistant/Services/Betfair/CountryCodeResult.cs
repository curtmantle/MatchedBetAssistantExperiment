using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.Services.Betfair
{
    public class CountryCodeResult
    {
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "marketCount")]
        public int MarketCount { get; set; }
    }
}
