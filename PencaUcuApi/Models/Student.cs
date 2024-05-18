namespace PencaUcuApi.Models;
public class Student
{
    public string StudentId { get; set; }
    public int Score { get; set; }
    public User User { get; set; }

    public Student(string studentId, int score, User user)
    {
        StudentId = studentId;
        Score = score;
        User = user;
    }
}
