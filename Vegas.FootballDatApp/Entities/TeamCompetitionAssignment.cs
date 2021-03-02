namespace Vegas.FootballDatApp.Entities
{
    public class TeamCompetitionAssignment : EntityBase
    {
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
    }
}
