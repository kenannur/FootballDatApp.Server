using Microsoft.EntityFrameworkCore;
using Npgsql;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.Contexts
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options)
        { }

        static FootballDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<CompetitionType>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<CompetitionType>();
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamCompetitionAssignment> TeamCompetitionAssignments { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Player> Players { get; set; }

        //public DbSet<Match> Matches { get; set; }

        //public DbSet<Referee> Referees { get; set; }

        //public DbSet<RefereeMatchAssignment> RefereeMatchAssignments { get; set; }

    }
}
