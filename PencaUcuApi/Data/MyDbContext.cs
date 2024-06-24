using Microsoft.EntityFrameworkCore;
using PencaUcuApi.DTOs;
using PencaUcuApi.Models;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Career> Careers { get; set; }
    public DbSet<GroupStage> GroupStages { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<MatchResult> MatchResults { get; set; }
    public DbSet<NationalTeam> NationalTeams { get; set; }
    public DbSet<NationalTeamGroupStage> NationalTeamGroupStages { get; set; }
    public DbSet<Prediction> Predictions { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentCareer> StudentCareers { get; set; }
    public DbSet<StudentTournamentPrediction> StudentTournamentPredictions { get; set; }
    public DbSet<StudentWithUserDTO> StudentScoreDTOs { get; set; }
    public DbSet<StudentWithUserDTO> StudentDTOs { get; set; }
    public DbSet<PredictionDTO> PredictionDTO { get; set; }
    public DbSet<FixtureItemDTO> FixtureItemDTO { get; set; }
    public DbSet<FixtureItem> FixtureItem { get; set; }
    public DbSet<PredictionItem> PredictionItem { get; set; }
    public DbSet<PredictionItemDTO> PredictionItemDTO { get; set; }
    public DbSet<MatchResultDTO> MatchResultsDTO { get; set; }
    public DbSet<PredictionResultItem> PredictionResultItem { get; set; }
    public DbSet<PredictionResultItemDTO> PredictionResultItemDTO { get; set; }
    public DbSet<TournamentResultDTO> TournamentResultDTO { get; set;}
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentWithUserDTO>().HasNoKey();
        modelBuilder.Entity<StudentDTO>().HasNoKey();
        modelBuilder.Entity<PredictionDTO>().HasNoKey();
        modelBuilder.Entity<FixtureItemDTO>().HasNoKey();
        modelBuilder.Entity<FixtureItem>().HasNoKey();
        modelBuilder.Entity<PredictionItemDTO>().HasNoKey();
        modelBuilder.Entity<PredictionItem>().HasNoKey();
        modelBuilder.Entity<PredictionResultItem>().HasNoKey();
        modelBuilder.Entity<PredictionResultItemDTO>().HasNoKey();
        modelBuilder.Entity<MatchResultDTO>().HasNoKey();
        modelBuilder.Entity<TournamentResultDTO>().HasNoKey();
    }
}
