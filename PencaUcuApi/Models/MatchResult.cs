namespace PencaUcuApi.Models;
public class MatchResult
{
    public long Id { get; set; }
    public long MatchId { get; set; }
    public int LocalNationalTeamGoals { get; set; }
    public int VisitorNationalTeamGoals { get; set; }
    public string WinnerId { get; set; }
    public MatchResult(long id, long matchId, int localNationalTeamGoals, int visitorNationalTeamGoals, string winnerId)
    {
        Id = id;
        MatchId = matchId;
        LocalNationalTeamGoals = localNationalTeamGoals;
        VisitorNationalTeamGoals = visitorNationalTeamGoals;
        WinnerId = winnerId;
    }
}