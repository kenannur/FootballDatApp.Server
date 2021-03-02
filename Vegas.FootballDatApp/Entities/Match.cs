using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vegas.FootballDatApp.Entities
{
    public class MatchDetail
    {
        public Match Match { get; set; }
    }

    public class Match : EntityBase
    {
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public DateTime? UtcDate { get; set; }

        public MatchStatus Status { get; set; }

        public string Venue { get; set; }

        public int? Matchday { get; set; }

        public string Stage { get; set; }

        public string Group { get; set; }

        [NotMapped]
        public Score Score { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public string Winner { get; set; }

        public string Duration { get; set; }

        public int? HomeTeamScoreH { get; set; }

        public int? AwayTeamScoreH { get; set; }

        public int? HomeTeamScoreF { get; set; }

        public int? AwayTeamScoreF { get; set; }

        public int? HomeTeamScoreE { get; set; }

        public int? AwayTeamScoreE { get; set; }

        public int? HomeTeamScoreP { get; set; }

        public int? AwayTeamScoreP { get; set; }

        [NotMapped]
        public List<Referee> Referees { get; set; }

        public virtual List<RefereeMatchAssignment> RefereeMatchAssignments { get; set; }
    }

    public enum MatchStatus
    {
        UNKNOWN = 0,
        SCHEDULED = 1,
        LIVE = 2,
        IN_PLAY = 3,
        PAUSED = 4,
        FINISHED = 5,
        POSTPONED = 6,
        SUSPENDED = 7,
        CANCELED = 8
    }

    public class ScoreDetail
    {
        public int? HomeTeam { get; set; }

        public int? AwayTeam { get; set; }
    }

    public class Score
    {
        public string Winner { get; set; }

        public string Duration { get; set; }

        public ScoreDetail FullTime { get; set; }

        public ScoreDetail HalfTime { get; set; }

        public ScoreDetail ExtraTime { get; set; }

        public ScoreDetail Penalties { get; set; }
    }
}
