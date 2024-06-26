namespace PencaUcuApi.DTOs;
public class StudentCareerDTO
{
    public string StudentId { get; set; }
    public string Career { get; set; }
    public StudentCareerDTO(string studentId, string career)
    {
        StudentId = studentId;
        Career = career;
    }
}