using System.ComponentModel.DataAnnotations;

// TODO: Actualizar modelos 

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