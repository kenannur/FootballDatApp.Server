using System;
using System.Collections.Generic;

namespace Vegas.FootballDatApp.Entities
{
    public class Team : EntityBase
    {
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Tla { get; set; }

        public string CrestUrl { get; set; }

        public int? Founded { get; set; }

        public string ClubColors { get; set; }

        public string Venue { get; set; }

        public DateTime? LastUpdated { get; set; }

        public virtual List<TeamCompetitionAssignment> TeamCompetitionAssignments { get; set; }

        public virtual List<Player> Squad { get; set; }
    }

    public class Standing
    {
        public string Stage { get; set; }

        public string Type { get; set; }

        public string Group { get; set; }

        public List<StandingRow> Table { get; set; }
    }

    public class StandingRow
    {
        public int Position { get; set; }

        public Team Team { get; set; }

        public int PlayedGames { get; set; }

        public string Form { get; set; }

        public int Won { get; set; }

        public int Draw { get; set; }

        public int Lost { get; set; }

        public int Points { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int GoalDifference { get; set; }
    }
}
