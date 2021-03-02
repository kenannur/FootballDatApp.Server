using System.Collections.Generic;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.Models.Response
{
    public class FetchCompetitionsResponse
    {
        public List<Competition> Competitions { get; set; }
    }
}
