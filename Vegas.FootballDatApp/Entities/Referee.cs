using System.Collections.Generic;

namespace Vegas.FootballDatApp.Entities
{
    public class Referee : EntityBase
    {
        public string Name { get; set; }

        public string Nationality { get; set; }

        public virtual List<RefereeMatchAssignment> RefereeMatchAssignments { get; set; }
    }
}
