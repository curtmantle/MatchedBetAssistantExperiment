using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.Services.Betfair
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketBettingType
    {
        ODDS,
        LINE,
        RANGE,
        ASIAN_HANDICAP_DOUBLE_LINE,
        ASIAN_HANDICAP_SINGLE_LINE,
        FIXED_ODDS
    }
}
