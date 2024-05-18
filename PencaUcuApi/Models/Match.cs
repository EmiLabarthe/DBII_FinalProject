namespace PencaUcuApi.Models;
public class Match
{
    public long Id { get; set; }
    public string LocalNationalTeam { get; set; }
    public string VisitorNationalTeam { get; set; }
    public DateTime Date { get; set; }
    public Match(long id, string localNationalTeam, string visitorNationalTeam, DateTime date)
    {
        Id = id;
        LocalNationalTeam = localNationalTeam;
        VisitorNationalTeam = visitorNationalTeam;
        Date = date;
    }
}