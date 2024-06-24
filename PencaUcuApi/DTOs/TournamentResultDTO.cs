namespace PencaUcuApi.DTOs;

public class TournamentResultDTO
{
    public int StudentId;
    public string ChampionId  { get; set; }
    public string ViceChampionId  { get; set; }

    public TournamentResultDTO() { }

    public TournamentResultDTO(int studentId, string championId, string viceChampionId)
    {
        StudentId = studentId;
        ChampionId = championId;
        ViceChampionId = viceChampionId;
    }
}