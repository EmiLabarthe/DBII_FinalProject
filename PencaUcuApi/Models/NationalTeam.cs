using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class NationalTeam
{
    [Key]
    public string Name { get; set; }
    public int Id { get; set; }
    public string GroupStageId { get; set; }

    public NationalTeam(int id, string name, string groupStageId)
    {
        Id = id;
        Name = name;
        GroupStageId = groupStageId;
    }
}