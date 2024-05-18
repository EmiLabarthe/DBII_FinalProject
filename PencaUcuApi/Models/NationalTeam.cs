namespace PencaUcuApi.Models;

// Modelo de prueba
public class NationalTeam
{
    public string Name { get; set; }
    public int Id { get; set; }

    public NationalTeam(int id, string name)
    {
        Id = id;
        Name = name;
    }
}