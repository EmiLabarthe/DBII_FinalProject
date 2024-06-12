using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class Stadium
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public Stadium(string id, string name, string state, string city)
    {
        Id = id;
        Name = name;
        State = state;
        City = city;
    }


}