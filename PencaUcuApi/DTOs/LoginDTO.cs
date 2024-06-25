namespace PencaUcuApi.DTOs;
public class LoginDTO
{
    public string Id { get; set; }
    public string Password { get; set; }
    public LoginDTO(string id, string password)
    {
        Id = id;
        Password = password;
    }
}