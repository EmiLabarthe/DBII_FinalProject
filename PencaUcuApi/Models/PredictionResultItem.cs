using System;
using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models
{
    public class PredictionResultItem
    {
        [Required]
        public string LocalNationalTeam { get; set; }

        [Required]
        public int LocalNationalTeamGoals { get; set; }

        public int? LocalNationalTeamPredictedGoals { get; set; } // might have not predicted

        [Required]
        public string VisitorNationalTeam { get; set; }

        [Required]
        public int VisitorNationalTeamGoals { get; set; }

        public int? VisitorNationalTeamPredictedGoals { get; set; }  // might have not predicted

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string StadiumName { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        public PredictionResultItem(
            string localNationalTeam,
            int localNationalTeamGoals,
            int? localNationalTeamPredictedGoals,
            string visitorNationalTeam,
            int visitorNationalTeamGoals,
            int? visitorNationalTeamPredictedGoals,
            DateTime date,
            string stadiumName,
            string state,
            string city
        )
        {
            LocalNationalTeam =
                localNationalTeam ?? throw new ArgumentNullException(nameof(localNationalTeam));
            LocalNationalTeamGoals = localNationalTeamGoals;
            LocalNationalTeamPredictedGoals = localNationalTeamPredictedGoals;
            VisitorNationalTeam =
                visitorNationalTeam ?? throw new ArgumentNullException(nameof(visitorNationalTeam));
            VisitorNationalTeamGoals = visitorNationalTeamGoals;
            VisitorNationalTeamPredictedGoals = visitorNationalTeamPredictedGoals;
            Date = date;
            StadiumName = stadiumName ?? throw new ArgumentNullException(nameof(stadiumName));
            State = state ?? throw new ArgumentNullException(nameof(state));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }
    }
}
