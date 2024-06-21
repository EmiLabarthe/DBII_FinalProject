namespace PencaUcuApi.DTOs;

public class MatchResultDTO
{
    public int Id;
    public long MatchId  { get; set; }
    public int LocalNationalTeamGoals { get; set; }
    public int VisitorNationalTeamGoals { get; set; }
    public string WinnerId  { get; set; }

    public MatchResultDTO() { }

    public MatchResultDTO(long matchId, int localNationalTeamGoals, int visitorNationalTeamGoals, string winnerId)
    {
        MatchId = matchId;
        LocalNationalTeamGoals = localNationalTeamGoals;
        VisitorNationalTeamGoals = visitorNationalTeamGoals;
        WinnerId = winnerId;
    }
}
