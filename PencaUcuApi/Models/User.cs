using System.ComponentModel.DataAnnotations;
using PencaUcuApi.DTOs;

namespace PencaUcuApi.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User(
        string id,
        string firstName,
        string lastName,
        string gender,
        string email,
        string password
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        Password = password;
    }

    public UserDTO ToDto()
    {
        return new UserDTO(Id, FirstName, LastName, Gender, Email, Password);
    }
}
