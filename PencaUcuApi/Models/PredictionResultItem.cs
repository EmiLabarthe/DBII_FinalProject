using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models
{
    public class PredictionResultItem
    {
        public string LocalNationalTeam { get; set; }
        public int LocalNationalTeamGoals { get; set; }
        public int LocalNationalTeamPredictedGoals { get; set; }
        public string VisitorNationalTeam { get; set; }
        public int VisitorNationalTeamGoals { get; set; }
        public int VisitorNationalTeamPredictedGoals { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
        public string StadiumName { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public PredictionResultItem(
            string localNationalTeam,
            int localNationalTeamGoals,
            int localNationalTeamPredictedGoals,
            string visitorNationalTeam,
            int visitorNationalTeamGoals,
            int visitorNationalTeamPredictedGoals,
            int points,
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
            LocalNationalTeamGoals = localNationalTeamGoals;
            LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
            VisitorNationalTeamGoals = visitorNationalTeamGoals;
            VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
            Points = points;
            Date = date;
            StadiumName = stadiumName ?? throw new ArgumentNullException(nameof(stadiumName));
            State = state ?? throw new ArgumentNullException(nameof(state));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }
    }
}
