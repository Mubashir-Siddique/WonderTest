using System;
using static GameAnalytics.Enumerations.Enumerations;

namespace WonderTest.Models
{
    public class SearchGameAnalyticModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EventType Event { get; set; }
        public Guid UserId { get; set; }

    }
}
