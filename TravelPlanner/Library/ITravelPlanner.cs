using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public interface ITravelPlanner
    {
        public ResultRoute GetNextRoute(List<BusRoute> busRoutes, string from, string to, string start);
    }
}
