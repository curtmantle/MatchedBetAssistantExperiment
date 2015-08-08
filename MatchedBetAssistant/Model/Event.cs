using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.Model
{
    public class Event
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string Timezone { get; set; }

        public string Venue { get; set; }

        public DateTime? OpenDate { get; set; }
    }
}
