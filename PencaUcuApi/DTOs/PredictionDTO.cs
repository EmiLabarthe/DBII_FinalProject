namespace PencaUcuApi.DTOs;

public class PredictionDTO
{
    
    public int? Id;
    public string? StudentId;
    public int? MatchId;
    public int? LocalNationalTeamPredictedGoals { get; set; }
    public int? VisitorNationalTeamPredictedGoals { get; set; }

    public PredictionDTO() { }

    public PredictionDTO(string studentId, int matchId, int localNationalTeamPredictedGoals, int visitorNationalTeamPredictedGoals)
    {
        StudentId = studentId;
        MatchId = matchId;
        LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
        VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
    }
}
