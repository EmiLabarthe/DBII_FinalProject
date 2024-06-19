namespace PencaUcuApi.DTOs
{
    public class PredictionResultItemDTO
    {
        public string LocalNationalTeam { get; set; }
        public int LocalNationalTeamGoals { get; set; }
        public int? LocalNationalTeamPredictedGoals { get; set; }
        public string VisitorNationalTeam { get; set; }
        public int VisitorNationalTeamGoals { get; set; }
        public int? VisitorNationalTeamPredictedGoals { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
        public string StadiumName { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public PredictionResultItemDTO() { }

        public PredictionResultItemDTO(
            string localNationalTeam,
            int localNationalTeamGoals,
            int? localNationalTeamPredictedGoals,
            string visitorNationalTeam,
            int visitorNationalTeamGoals,
            int? visitorNationalTeamPredictedGoals,
            int points,
            DateTime date,
            string stadiumName,
            string state,
            string city
        )
        {
            LocalNationalTeam = localNationalTeam;
            LocalNationalTeamGoals = localNationalTeamGoals;
            LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
            VisitorNationalTeam = visitorNationalTeam;
            VisitorNationalTeamGoals = visitorNationalTeamGoals;
            VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
            Points = points;
            Date = date;
            StadiumName = stadiumName;
            State = state;
            City = city;
        }
    }
}
