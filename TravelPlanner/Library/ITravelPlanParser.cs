using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public interface ITravelPlanParser
    {
        public List<BusRoute> ParseJson(string text);
    }
}
