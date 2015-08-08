
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

        public IList<Event> GetEventsForEventTypeWithinCountry(string eventTypeId, string countryCode)
        {
            MarketFilter filter = new MarketFilter();
            if (!string.IsNullOrEmpty(eventTypeId))
            {
                filter.EventTypeIds = new HashSet<string>() { eventTypeId };
            }
            if (!string.IsNullOrEmpty(countryCode))
            {
                filter.MarketCountries = new HashSet<string>() { countryCode };
            }

            return CreateEvents(filter);
        }

        public IList<Event> GetEventsForCompetition(string competitionId)
        {
            MarketFilter filter = new MarketFilter();
            if (!string.IsNullOrEmpty(competitionId))
            {
                filter.CompetitionIds = new HashSet<string>() { competitionId };
            }

            return CreateEvents(filter);
        }

        private IList<Event> CreateEvents(MarketFilter filter)
        {
            var events = this.accountClient.ListEvents(filter).Where(e => e.MarketCount > 0).OrderBy(e => e.Event.OpenDate);

            var modelEvents = new List<Event>();
            foreach (var modelEvent in events)
            {
                modelEvents.Add(new Event()
                {
                    Id = modelEvent.Event.Id,
                    Name = modelEvent.Event.Name,
                    CountryCode = modelEvent.Event.CountryCode,
                    Timezone = modelEvent.Event.Timezone,
                    OpenDate = modelEvent.Event.OpenDate,
                    Venue = modelEvent.Event.Venue
                });
            }

            return modelEvents;
        }

        public IList<Competition> GetCompetitionsForEventTypeWithinCountry(string eventTypeId, string countryCode)
        {
            MarketFilter filter = new MarketFilter();
            if (!string.IsNullOrEmpty(eventTypeId))
            {
                filter.EventTypeIds = new HashSet<string>() { eventTypeId };
            }
            if (!string.IsNullOrEmpty(countryCode))
            {
                filter.MarketCountries = new HashSet<string>() { countryCode };
            }

            var competitions = this.accountClient.ListCompetitions(filter);

            var modelCompetitions = new List<Competition>();
            foreach (var competition in competitions)
            {
                modelCompetitions.Add(new Competition()
                {
                    Id = competition.Competition.Id,
                    Name = competition.Competition.Name
                });
            }

            return modelCompetitions;
        }

        public string GetMarketCatalogueForEvent(string eventId)
        {
            MarketFilter filter = new MarketFilter();
            if (!string.IsNullOrEmpty(eventId))
            {
                filter.EventIds = new HashSet<string>() { eventId };
            }

            var market = this.accountClient.ListMarketCatalogue(filter);

            Console.WriteLine("Total markets = {0}", market.Count);

            return "DONE";
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