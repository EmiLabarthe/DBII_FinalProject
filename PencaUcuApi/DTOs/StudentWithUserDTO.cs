namespace PencaUcuApi.DTOs;
public class StudentWithUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Score { get; set; }
    public StudentWithUserDTO(string firstName, string lastName, int score)
    {
        FirstName = firstName;
        LastName = lastName;
        Score = score;
    }
}