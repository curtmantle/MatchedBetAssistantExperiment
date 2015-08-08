using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchedBetAssistant.Services.Betfair
{
    public class TimeRange
    {
        [JsonProperty(PropertyName = "from")]
        public DateTime From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public DateTime To { get; set; }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}", "TimeRange")
                        .AppendFormat(" : From={0}", From)
                        .AppendFormat(" : To={0}", To)
                        .ToString();
        }
    }
}
