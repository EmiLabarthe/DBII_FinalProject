using Microsoft.EntityFrameworkCore;
using PencaUcuApi.Models;

namespace PencaUcuApi.DTOs;
public class NotificationDTO
{
    public string Email { get; set; }
    public string? predId { get; set; }

    public NotificationDTO(string Email, string predId)
    {
        this.Email = Email;
        this.predId = predId;
    }
}