namespace PencaUcuApi.DTOs;
public class StudentDTO
{
    public string StudentId { get; set; }
    public int Score { get; set; }
    public StudentDTO(string studentId, int score)
    {
        StudentId = studentId;
        Score = score;
    }
}