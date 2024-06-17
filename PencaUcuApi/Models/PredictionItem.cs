using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models
{
    public class PredictionItem
    {
        public string LocalNationalTeam { get; set; }
        public int LocalNationalTeamPredictedGoals { get; set; }
        public string VisitorNationalTeam { get; set; }
        public int VisitorNationalTeamPredictedGoals { get; set; }
        public DateTime Date { get; set; }
        public string StadiumName { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public PredictionItem(
            string localNationalTeam,
            int LocalNationalTeamPredictedGoals,
            string visitorNationalTeam,
            int VisitorNationalTeamPredictedGoals,
            DateTime date,
            string stadiumName,
            string state,
            string city
        )
        {
            LocalNationalTeam =
                localNationalTeam ?? throw new ArgumentNullException(nameof(localNationalTeam));
            VisitorNationalTeam =
                visitorNationalTeam ?? throw new ArgumentNullException(nameof(visitorNationalTeam));
            Date = date;
            StadiumName = stadiumName ?? throw new ArgumentNullException(nameof(stadiumName));
            State = state ?? throw new ArgumentNullException(nameof(state));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }
    }
}
