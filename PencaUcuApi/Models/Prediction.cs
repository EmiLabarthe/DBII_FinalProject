using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class Prediction
{
    [Key]
    public long Id { get; set; }
    public string StudentId { get; set; }
    public long MatchId { get; set; }
    public int LocalNationalTeamPredictedGoals { get; set; }
    public int VisitorNationalTeamPredictedGoals { get; set; }
    public Prediction(long id, string studentId, long matchId, int localNationalTeamPredictedGoals, int visitorNationalTeamPredictedGoals)
    {
        Id = id;
        StudentId = studentId;
        MatchId = matchId;
        LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
        VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
    }
}