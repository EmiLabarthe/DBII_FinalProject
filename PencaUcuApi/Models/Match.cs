using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class Match
{
    [Key]
    public long Id { get; set; }
    public string LocalNationalTeam { get; set; }
    public string VisitorNationalTeam { get; set; }
    public DateTime Date { get; set; }
    public long StadiumId { get; set; }
    public string StageId { get; set; }
    public Match(long id, string localNationalTeam, string visitorNationalTeam, DateTime date, long stadiumId, string stageId)
    {
        Id = id;
        LocalNationalTeam = localNationalTeam;
        VisitorNationalTeam = visitorNationalTeam;
        Date = date;
        StadiumId = stadiumId;
        StageId = stageId;
    }
}