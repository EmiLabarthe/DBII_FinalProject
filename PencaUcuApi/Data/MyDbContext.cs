using Microsoft.EntityFrameworkCore;
using PencaUcuApi.Models;
public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Career> Careers { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<MatchResult> MatchResults { get; set; }
    public DbSet<NationalTeam> NationalTeams { get; set; }
    public DbSet<NationalTeamGroupStage> NationalTeamGroupStages { get; set; }
    public DbSet<Prediction> Predictions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentCareer> StudentCareers { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
