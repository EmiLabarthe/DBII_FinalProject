using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class GroupStage
{
    [Key]
    public string Id { get; set; }
    public GroupStage(string id)
    {
        Id = id;
    }
}