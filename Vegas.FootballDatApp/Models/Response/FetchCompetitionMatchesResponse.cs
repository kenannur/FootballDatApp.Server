using System.Collections.Generic;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.Models.Response
{
    public class FetchCompetitionMatchesResponse
    {
        public Competition Competition { get; set; }

        public List<Match> Matches { get; set; }
    }
}
