namespace PencaUcuApi.DTOs;

public class MatchResultDTO
{
    public int Id;
    public long MatchId;
    public int LocalNationalTeamGoals { get; set; }
    public int VisitorNationalTeamGoals { get; set; }
    public string WinnerId;

    public MatchResultDTO() { }

    public MatchResultDTO(long matchId, int localNationalTeamGoals, int visitorNationalTeamGoals, string winnerId)
    {
        MatchId = matchId;
        LocalNationalTeamGoals = localNationalTeamGoals;
        VisitorNationalTeamGoals = visitorNationalTeamGoals;
        WinnerId = winnerId;
    }
}
