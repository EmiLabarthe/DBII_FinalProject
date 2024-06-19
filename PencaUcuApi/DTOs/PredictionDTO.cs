namespace PencaUcuApi.DTOs;

public class PredictionDTO
{
    
    public int Id;
    public string StudentId  { get; set; }
    public long MatchId { get; set; }
    public int LocalNationalTeamPredictedGoals { get; set; }
    public int VisitorNationalTeamPredictedGoals { get; set; }

    public PredictionDTO() { }

    public PredictionDTO(string studentId, long matchId, int localNationalTeamPredictedGoals, int visitorNationalTeamPredictedGoals)
    {
        StudentId = studentId;
        MatchId = matchId;
        LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
        VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
    }
}
