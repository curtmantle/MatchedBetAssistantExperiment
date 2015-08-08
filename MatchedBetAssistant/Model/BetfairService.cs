﻿
using MatchedBetAssistant.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MatchedBetAssistant.Services.Betfair;
namespace MatchedBetAssistant.Model
{
    public class BetfairService : IDisposable
    {
        private BetfairAccountClient accountClient;
        private string applicationId;
        private string sessionToken;

        public BetfairService()
        {
            this.applicationId = ReadApplicationKey();
        }

        public BetfairAccount GetBetfairAccount()
        {
            var result = this.accountClient.GetAccountDetails();

            var fundsResult = this.accountClient.GetAccountFunds();

            var account = new BetfairAccount()
            {
                Name = string.Format("{0} {1}", result.FirstName, result.LastName),
                Balance = fundsResult.AvailableToBetBalance
            };

            return account;
        }

        public BetfairAccount Login(string sessionToken)
        {
            this.sessionToken = sessionToken;

            this.accountClient = new BetfairAccountClient(applicationId, sessionToken);

            return GetBetfairAccount();
        }

        public IList<EventType> GetEventTypes(IEnumerable<string> eventTypeIds)
        {
            ISet<string> filterIds = eventTypeIds != null ? new HashSet<string>(eventTypeIds): new HashSet<string>();

            var marketFilter = new MarketFilter() { EventTypeIds = filterIds };
            var eventTypes = this.accountClient.ListEventTypes(marketFilter);

            var modelEventTypes = new List<EventType>();
            foreach(var eventType in eventTypes)
            {
                modelEventTypes.Add(new EventType() { Id = eventType.EventType.Id, Name = eventType.EventType.Name });
            }
            return modelEventTypes;
        }

        public IList<Country> GetCountriesForEventType(string eventTypeId)
        {
            MarketFilter filter = new MarketFilter();
            if (!string.IsNullOrEmpty(eventTypeId))
            {
                filter.EventTypeIds = new HashSet<string>() { eventTypeId };
            }

            var countryCodes = this.accountClient.ListCountryCodes(filter).Where(c=>c.MarketCount > 0);

            var modelCountries = new List<Country>();
            foreach(var countryCode in countryCodes)
            {
                modelCountries.Add(new Country() { CountryCode = countryCode.CountryCode, MarketCount = countryCode.MarketCount });
            }
            return modelCountries;
        }

        public IList<EventType> GetEventTypes()
        {
            return GetEventTypes(null);
        }

        public string ReadApplicationKey()
        {
            var fileName = System.Environment.CurrentDirectory + "\\app.key";
            if (!File.Exists(fileName))
            {
                Console.WriteLine(@"Bother");
            }
            var iniFile = new StreamReader(fileName);

            string key = iniFile.ReadToEnd();

            iniFile.Close();

            return key;
        }

        public void Dispose()
        {
            if (accountClient != null)
            {
                accountClient.Dispose();
                accountClient = null;
            }
        }
    }
}