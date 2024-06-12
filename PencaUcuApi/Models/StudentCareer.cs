using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class StudentCareer
{
    [Key]
    public string StudentId { get; set; }
    public string CareerId { get; set; }

    public StudentCareer(string studentId, string careerId)
    {
        StudentId = studentId;
        CareerId = careerId;
    }
}