namespace PencaUcuApi.DTOs;

public class PredictionItemDTO
{
    public string? LocalNationalTeam { get; set; }
    public string? LocalNationalTeamFlagURL { get; set; }
    public int? LocalNationalTeamGoals { get; set; }
    public string? VisitorNationalTeam { get; set; }
    public string? VisitorNationalTeamFlagURL { get; set; }
    public int? VisitorNationalTeamGoals { get; set; }
    public DateTime Date { get; set; }
    public string? StadiumName { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }

    public PredictionItemDTO() { }

    public PredictionItemDTO(
        string localTeam,
        string localNationalTeamFlagURL,
        int localNationalTeamGoals,
        string visitorTeam, 
        string visitorNationalTeamFlagURL,
        int visitorNationalTeamGoals,
        DateTime matchDate,
        string stadium,
        string state,
        string city
    )
    {
        LocalNationalTeam = localTeam;
        LocalNationalTeamFlagURL = localNationalTeamFlagURL;
        LocalNationalTeamGoals = localNationalTeamGoals;
        VisitorNationalTeam = visitorTeam;
        VisitorNationalTeamFlagURL = visitorNationalTeamFlagURL;
        VisitorNationalTeamGoals = visitorNationalTeamGoals;
        Date = matchDate;
        StadiumName = stadium;
        State = state;
        City = city;
    }
}
