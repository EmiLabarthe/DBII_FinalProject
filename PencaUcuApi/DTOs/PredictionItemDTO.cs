using PencaUcuApi.Models;

namespace PencaUcuApi.DTOs;

public class PredictionItemDTO
{
    public string LocalNationalTeam { get; set; }
    public int? LocalNationalTeamPredictedGoals { get; set; }
    public string VisitorNationalTeam { get; set; }
    public int? VisitorNationalTeamPredictedGoals { get; set; }
    public DateTime Date { get; set; }
    public string StadiumName { get; set; }
    public string State { get; set; }
    public string City { get; set; }

    public PredictionItemDTO() { }

    public PredictionItemDTO(
        string localTeam,
        int? localNationalTeamPredictedGoals,
        string visitorTeam,
        int? visitorNationalTeamPredictedGoals,
        DateTime matchDate,
        string stadium,
        string state,
        string city
    )
    {
        LocalNationalTeam = localTeam;
        LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
        VisitorNationalTeam = visitorTeam;
        VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
        Date = matchDate;
        StadiumName = stadium;
        State = state;
        City = city;
    }
}
