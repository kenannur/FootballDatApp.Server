namespace Vegas.FootballDatApp.Entities
{
    public class Area : EntityBase
    {
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string EnsignUrl { get; set; }

        public int? ParentAreaId { get; set; }
    }
}
