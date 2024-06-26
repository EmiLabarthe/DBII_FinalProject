using Microsoft.EntityFrameworkCore;
using PencaUcuApi.Models;

namespace PencaUcuApi.DTOs;
public class TodayMatchesDTO
{
    public int cantPartidosHoy { get; set; }

    public TodayMatchesDTO(int cantPartidosHoy)
    {
        this.cantPartidosHoy = cantPartidosHoy;
    }
}