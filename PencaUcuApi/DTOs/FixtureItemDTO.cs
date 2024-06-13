namespace PencaUcuApi.DTOs;
public class FixtureItemDTO
{
    public string? LocalNationalTeam { get; set; }
    public string? VisitorNationalTeam { get; set; }
    public DateTime Date { get; set; }
    public string? StadiumName { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    
    public FixtureItemDTO(){
        
    }
    public FixtureItemDTO(string localTeam, string visitorTeam, DateTime matchDate, string stadium, string state, string city)
    {
        LocalNationalTeam = localTeam;
        VisitorNationalTeam = visitorTeam;
        Date = matchDate;
        StadiumName = stadium;
        State = state;
        City = city;
    }
}