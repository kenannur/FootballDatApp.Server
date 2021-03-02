using System;

namespace Vegas.FootballDatApp.Entities
{
    public class Season : EntityBase
    {
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? CurrentMatchday { get; set; }

        public int? WinnerId { get; set; }
        public virtual Team Winner { get; set; }
    }
}
