using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models
{
    public class PredictionItem
    {
        public long? PredictionId { get; set; }
        public long MatchId { get; set; }
        public string LocalNationalTeam { get; set; }
        public int? LocalNationalTeamPredictedGoals { get; set; }
        public string VisitorNationalTeam { get; set; }
        public int? VisitorNationalTeamPredictedGoals { get; set; }
        public DateTime Date { get; set; }
        public string StadiumName { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public PredictionItem(
            long? predictionId,
            long matchId,
            string localNationalTeam,
            int? localNationalTeamPredictedGoals,
            string visitorNationalTeam,
            int? visitorNationalTeamPredictedGoals,
            DateTime date,
            string stadiumName,
            string state,
            string city
        )
        {
            PredictionId = predictionId;
            MatchId = matchId;
            LocalNationalTeam =
                localNationalTeam ?? throw new ArgumentNullException(nameof(localNationalTeam));
            VisitorNationalTeam =
                visitorNationalTeam ?? throw new ArgumentNullException(nameof(visitorNationalTeam));
            LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
            VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
            Date = date;
            StadiumName = stadiumName ?? throw new ArgumentNullException(nameof(stadiumName));
            State = state ?? throw new ArgumentNullException(nameof(state));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }
    }
}
