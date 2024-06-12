using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class Career
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }

    public Career(long id, string name)
    {
        Id = id;
        Name = name;
    }
}