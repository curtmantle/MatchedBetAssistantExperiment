using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.Model.Betfair.Account
{
    public class AccountFunds
    {
        public double AvailableToBetBalance { get; set; }

        public double Exposure { get; set; }

        public double RetainedCommission { get; set; }

        public double ExposureLimit { get; set; }

        public double DiscountRate { get; set; }

        public int PointsBalance { get; set; }
    }
}
