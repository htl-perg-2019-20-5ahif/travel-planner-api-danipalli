using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class TravelPlanner : ITravelPlanner
    {
        public ResultRoute GetNextRoute(List<BusRoute> busRoutes, string from, string to, string start)
        {
            from = from.ToLower();
            to = to.ToLower();

            ResultRoute resultRoute;

            if (from.Equals("linz"))
            {
                resultRoute = GetRouteFromLinz(busRoutes, to, DateTime.Parse(start));
            }
            else
            {
                resultRoute = GetRouteToLinz(busRoutes, from, DateTime.Parse(start));
                if (!to.Equals("linz") && resultRoute != null)
                {
                    var secondRoute = GetRouteFromLinz(busRoutes, to, DateTime.Parse(resultRoute.ArrivalTime));
                    if(secondRoute != null)
                    {
                        resultRoute.Arrive = secondRoute.Arrive;
                        resultRoute.ArrivalTime = secondRoute.ArrivalTime;
                    }
                    else
                    {
                        return secondRoute;
                    }
                }
            }
            return resultRoute;
        }

        private static ResultRoute GetRouteFromLinz(IEnumerable<BusRoute> busRoutes, string to, DateTime start)
        {
            ResultRoute resultRoute = new ResultRoute();

            foreach (var route in busRoutes)
            {
                if (to.Equals(route.City.ToLower()))
                {
                    foreach (var connection in route.FromLinz)
                    {
                        if (DateTime.Parse(connection.Leave).CompareTo(start) >= 0)
                        {
                            resultRoute.Depart = "Linz";
                            resultRoute.DepartureTime = connection.Leave;
                            resultRoute.Arrive = route.City;
                            resultRoute.ArrivalTime = connection.Arrive;
                            return resultRoute;
                        }
                    }
                }
            }
            return null;
        }

        private static ResultRoute GetRouteToLinz(List<BusRoute> busRoutes, string from, DateTime start)
        {
            ResultRoute resultRoute = new ResultRoute();

            foreach(var route in busRoutes)
            {
                if (from.Equals(route.City.ToLower()))
                {
                    foreach (var connection in route.ToLinz)
                    {
                        if (DateTime.Parse(connection.Leave).CompareTo(start) >= 0)
                        {
                            resultRoute.Depart = route.City;
                            resultRoute.DepartureTime = connection.Leave;
                            resultRoute.Arrive = "Linz";
                            resultRoute.ArrivalTime = connection.Arrive;
                            return resultRoute;
                        }
                    }
                }
            }
            return null;
        }
    }
}
