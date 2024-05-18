namespace PencaUcuApi.Models;
public class StudentCareer
{
    public string StudentId { get; set; }
    public string CareerId { get; set; }

    public StudentCareer(string studentId, string careerId)
    {
        StudentId = studentId;
        CareerId = careerId;
    }
}