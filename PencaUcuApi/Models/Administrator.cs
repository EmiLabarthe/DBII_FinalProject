namespace PencaUcuApi.Models;
public class Administrator
{
    public string AdminId { get; set; }
    public Administrator(string AdminId)
    {
        this.AdminId = AdminId;
    }
}