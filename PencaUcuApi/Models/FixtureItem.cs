using System.ComponentModel.DataAnnotations;

namespace PencaUcuApi.Models
{
    public class FixtureItem
    {
        public string LocalNationalTeam { get; set; }
        public string VisitorNationalTeam { get; set; }
        public DateTime Date { get; set; }
        public string StadiumName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public FixtureItem(string localNationalTeam, string visitorNationalTeam, DateTime date, string stadiumName, string state, string city)
        {
            LocalNationalTeam = localNationalTeam;
            VisitorNationalTeam = visitorNationalTeam;
            Date = date;
            StadiumName = stadiumName;
            State = state;
            City = city;
        }
}
}