using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Library
{
    public class TravelPlanParser : ITravelPlanParser
    {
        public List<BusRoute> ParseJson(string text)
        {
            return JsonSerializer.Deserialize<List<BusRoute>>(text);
        }
    }
}
