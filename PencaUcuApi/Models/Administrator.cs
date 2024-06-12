using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models;
public class Administrator
{
    [Key]
    public string AdminId { get; set; }
    public Administrator(string AdminId)
    {
        this.AdminId = AdminId;
    }
}