namespace PencaUcuApi.Models;
public class Administrator
{
    public string AdminId { get; set; }
    public User User { get; set; }

    public Administrator(string adminId, User user)
    {
        AdminId = adminId;
        User = user;
    }
}