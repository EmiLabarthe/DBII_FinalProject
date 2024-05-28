using System.ComponentModel.DataAnnotations;
using PencaUcuApi.DTOs;

namespace PencaUcuApi.Models;
public class Student
{
    [Key]
    public string StudentId { get; set; }
    public int Score { get; set; }
    public Student(string studentId, int score)
    {
        StudentId = studentId;
        Score = score;
    }

    public StudentDTO ToDto()
    {
        return new StudentDTO(StudentId, Score);
    }
}
