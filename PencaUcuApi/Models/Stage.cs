using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class Stage
{
    [Key]
    public string Id { get; set; }
    public Stage(string id)
    {
        Id = id;
    }
}