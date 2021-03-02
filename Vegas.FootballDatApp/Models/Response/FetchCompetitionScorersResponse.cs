using System.Collections.Generic;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.Models.Response
{
    public class FetchCompetitionScorersResponse
    {
        public Competition Competition { get; set; }

        public Season Season { get; set; }

        public List<Scorer> Scorers { get; set; }
    }
}
