using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models
{
    public class PredictionItem
    {
        public string LocalNationalTeam { get; set; }
        public int LocalNationalTeamGoals { get; set; }
        public string VisitorNationalTeam { get; set; }
        public int VisitorNationalTeamGoals { get; set; }
        public DateTime Date { get; set; }
        public string StadiumName { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public PredictionItem(
            string localNationalTeam,
            int localNationalTeamGoals,
            string visitorNationalTeam,
            int visitorNationalTeamGoals,
            DateTime date,
            string stadiumName,
            string state,
            string city
        )
        {
            LocalNationalTeam =
                localNationalTeam ?? throw new ArgumentNullException(nameof(localNationalTeam));
            LocalNationalTeamGoals = localNationalTeamGoals;
            VisitorNationalTeam =
                visitorNationalTeam ?? throw new ArgumentNullException(nameof(visitorNationalTeam));
            VisitorNationalTeamGoals = visitorNationalTeamGoals;
            Date = date;
            StadiumName = stadiumName ?? throw new ArgumentNullException(nameof(stadiumName));
            State = state ?? throw new ArgumentNullException(nameof(state));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }
    }
}
