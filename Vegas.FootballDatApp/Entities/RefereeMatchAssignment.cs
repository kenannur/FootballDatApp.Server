namespace Vegas.FootballDatApp.Entities
{
    public class RefereeMatchAssignment : EntityBase
    {
        public int RefereeId { get; set; }
        public virtual Referee Referee { get; set; }

        public int MatchId { get; set; }
        public virtual Match Match { get; set; }

        public string RefereeRole { get; set; }
    }
}
