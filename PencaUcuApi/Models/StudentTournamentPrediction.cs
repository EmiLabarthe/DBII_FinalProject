namespace PencaUcuApi.Models;
public class StudentTournamentPrediction
{
    public string StudentId { get; set; }
    public string ChampionId { get; set; }
    public string ViceChampionId { get; set; }

    public StudentTournamentPrediction(string studentId, string championId, string viceChampionId)
    {
        StudentId = studentId;
        ChampionId = championId;
        ViceChampionId = viceChampionId;
    }
}