using System.Collections.Generic;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.Models.Response
{
    public class FetchTeamMatchesResponse
    {
        public List<Match> Matches { get; set; }
    }
}
