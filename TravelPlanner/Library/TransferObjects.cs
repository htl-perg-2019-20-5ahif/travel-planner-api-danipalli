using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;


namespace Library
{
    public class BusRoute
    {
        [JsonPropertyName("City")]
        public string City { get; set; }

        [JsonPropertyName("ToLinz")]
        public List<Connection> ToLinz { get; set; }

        [JsonPropertyName("FromLinz")]
        public List<Connection> FromLinz { get; set; }
    }

    public class Connection
    {
        [JsonPropertyName("Leave")]
        public string Leave { get; set; }

        [JsonPropertyName("Arrive")]
        public string Arrive { get; set; }
    }

    public class ResultRoute
    {
        [JsonPropertyName("depart")]
        public string Depart { get; set; }

        [JsonPropertyName("departureTime")]
        public string DepartureTime { get; set; }

        [JsonPropertyName("arrive")]
        public string Arrive { get; set; }

        [JsonPropertyName("arrivalTime")]
        public string ArrivalTime { get; set; }
    }
}
