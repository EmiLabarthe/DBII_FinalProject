using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;

public class NationalTeamGroupStage
{
    [Key]
    public long Id { get; set; }
    public string NationalTeamId { get; set; }
    public string GroupStageId { get; set; }
    public int Points { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalsDifference { get; set; }

    public NationalTeamGroupStage(long id, string nationalTeamId, string groupStageId, int points, int wins, int draws, int losses, int goalsFor, int goalsAgainst, int goalsDifference)
    {
        Id = id;
        NationalTeamId = nationalTeamId;
        GroupStageId = groupStageId;
        Points = points;
        Wins = wins;
        Draws = draws;
        Losses = losses;
        GoalsFor = goalsFor;
        GoalsAgainst = goalsAgainst;
        GoalsDifference = goalsDifference;
    }
}