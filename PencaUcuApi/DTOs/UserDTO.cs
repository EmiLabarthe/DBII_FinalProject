namespace PencaUcuApi.DTOs;
public class UserDTO
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Password { get; set; }
    public string Career { get; set; }
    public UserDTO(string id, string firstName, string lastName, string gender, string email, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Gender = gender;
        Password = password;
    }
}