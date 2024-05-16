namespace PencaUcuApi.Models;

// Modelo de prueba
public class Seleccion
{
    private string name { get; set; }
    private string id { get; set; }

    public Seleccion(string id, string name)
    {
        this.id = id;
        this.name = name;
    }
}