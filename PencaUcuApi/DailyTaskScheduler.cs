using System;
using System.Threading;
using PencaUcuApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using dotenv.net;
namespace PencaUcuApi.Scheduler;
class DailyTaskScheduler
{
    private Timer _timer;
    public static string connectionString { get; set; }
    public void Start()
    {
        ScheduleNextExecution();
    }

    private void ScheduleNextExecution()
    {
        DateTime now = DateTime.Now;
        DateTime nextExecution = now.Date.AddHours(20);

        // Si ahora es > a la próxima ejecución, se fija para mañana
        if (now >= nextExecution)
        {
            nextExecution = nextExecution.AddDays(1);
        }

        TimeSpan initialDelay = nextExecution - now;

        _timer = new Timer(ExecuteDailyTask, null, initialDelay, Timeout.InfiniteTimeSpan);
    }

    private void ExecuteDailyTask(object state)
    {
        getEmailsForNotification(connectionString);
        sendEmails();
        // Preparar próxima ejecución
        _timer.Change(TimeSpan.FromDays(1), Timeout.InfiniteTimeSpan);
    }

    private static IList<string> emailsToSend = new List<string>();
    private static void sendEmails()
    {
        SmtpClient smtpClient = createSMTPClient();
        if (smtpClient != null)
        {
            MailMessage mailMessage = new MailMessage();
            string? email = Environment.GetEnvironmentVariable("mail");

            mailMessage.From = new MailAddress(email);
            mailMessage.Subject = "Recordatorio penca!";
            mailMessage.Body = "Hay partidos de hoy por predecir!";
            foreach (string mail in emailsToSend)
            {
                mailMessage.To.Add(mail);
            }
            // Manu: Puse mi mail para corrobar que se envien
            mailMessage.To.Add("manuel5mongelos@gmail.com");
            if (emailsToSend.Count() > 0)
            {
                smtpClient.Send(mailMessage);
            }
        }
        else
        {
            Console.WriteLine("Authentication failed!");
        }
    }
    public static void getEmailsForNotification(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
        emailsToSend.Clear();
        optionsBuilder.UseMySql(connectionString, serverVersion);
        MyDbContext context = new MyDbContext(optionsBuilder.Options);

        try
        {
            List<NotificationDTO> queryResult = context.NotificationDTO.FromSqlRaw($"WITH partidosHoy as (Select Id from Matches m where Date(m.Date) = CURDATE()) SELECT Email, P.Id as predId from Users u JOIN UcuPenca2024.Students S on u.Id = S.StudentId left join UcuPenca2024.Predictions P on S.StudentId = P.StudentId and P.MatchId in (Select Id from partidosHoy)").ToList();
            TodayMatchesDTO todayMatchesCount = context.TodayMatchesDTO.FromSqlRaw("Select count(*) as cantPartidosHoy from Matches m where Date(m.Date) = CURDATE()").FirstOrDefault();
            int matchesAmount = 0;
            if (todayMatchesCount != null)
            {
                matchesAmount = todayMatchesCount.cantPartidosHoy;
            }
            Dictionary<string, int> emailCount = new Dictionary<string, int>();

            foreach (var result in queryResult)
            {
                if (!emailCount.ContainsKey(result.Email))
                {
                    emailCount[result.Email] = 0;
                }
                else
                {
                    emailCount[result.Email]++;
                }
            }
            foreach (var pair in emailCount)
            {
                if (pair.Value < matchesAmount)
                {
                    Console.WriteLine(pair.Key);
                    emailsToSend.Add(pair.Key);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static SmtpClient createSMTPClient()
    {
        SmtpClient? smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        // Carga desde .env las credenciales
        DotEnv.Load();
        string? pass = Environment.GetEnvironmentVariable("contra");
        string? mail = Environment.GetEnvironmentVariable("mail");
        if (pass != null)
            smtpClient.Credentials = new NetworkCredential(mail, pass);
        else
            smtpClient = null;
        return smtpClient;
    }

}