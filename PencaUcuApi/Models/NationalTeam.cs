using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class NationalTeam
{
    [Key]
    public string Name { get; set; }
    public int Id { get; set; }

    public NationalTeam(int id, string name)
    {
        Id = id;
        Name = name;
    }
}