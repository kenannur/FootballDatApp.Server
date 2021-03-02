using System;
using System.Collections.Generic;

namespace Vegas.FootballDatApp.Entities
{
    public class Competition : EntityBase
    {
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string EmblemUrl { get; set; }

        public string Plan { get; set; }

        public CompetitionType? Type { get; set; }

        public DateTime? LastUpdated { get; set; }

        //public int? CurrentSeasonId { get; set; }

        public virtual List<TeamCompetitionAssignment> TeamCompetitionAssignments { get; set; }

        public virtual List<Season> Seasons { get; set; }
    }

    public enum CompetitionType
    {
        None,
        League,
        Cup 
    }

    public enum CompetitionStage
    {
        NONE,
        REGULAR_SEASON,
        GROUP_STAGE,
        ROUND_OF_16,
        QUARTER_FINALS,
        SEMI_FINALS,
        FINAL
    }
}
