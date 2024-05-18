namespace PencaUcuApi.Models;
public class NationalTeamGroupStage
{
    public long Id { get; set; }
    public string NationalTeamId { get; set; } // Ponemos esto o el NationalTeam como objeto?
    public int Points { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalsDifference { get; set; }

    public NationalTeamGroupStage(long id, string nationalTeamId, int points, int wins, int draws, int losses, int goalsFor, int goalsAgainst, int goalsDifference)
    {
        Id = id;
        NationalTeamId = nationalTeamId;
        Points = points;
        Wins = wins;
        Draws = draws;
        Losses = losses;
        GoalsFor = goalsFor;
        GoalsAgainst = goalsAgainst;
        GoalsDifference = goalsDifference;
    }
}