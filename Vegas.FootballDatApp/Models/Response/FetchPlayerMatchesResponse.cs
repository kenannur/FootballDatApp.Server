using System.Collections.Generic;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.Models.Response
{
    public class FetchPlayerMatchesResponse
    {
        public Player Player { get; set; }

        public List<Match> Matches { get; set; }
    }
}
