using System;

namespace Vegas.FootballDatApp.Entities
{
    public class Player : EntityBase
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string CountryOfBirth { get; set; }

        public string Nationality { get; set; }

        public string Position { get; set; }

        public int? ShirtNumber { get; set; }

        public string Role { get; set; }

        public DateTime? LastUpdated { get; set; }
    }

    public class Scorer
    {
        public Player Player { get; set; }

        public Team Team { get; set; }

        public int NumberOfGoals { get; set; }
    }
}
