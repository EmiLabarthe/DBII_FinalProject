using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class User
{
    [Key]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public User(string id, string firstName, string lastName, string gender, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
    }
}