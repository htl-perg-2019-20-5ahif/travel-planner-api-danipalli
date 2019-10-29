using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Library;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class TravelPlannerController : ControllerBase
    {
        private static List<BusRoute> _busRoutes;
        private readonly ITravelPlanner _planner;
        private readonly ITravelPlanParser _parser;
        private readonly IHttpClientFactory _clientFactory;

        public TravelPlannerController(ITravelPlanner planner, ITravelPlanParser parser, IHttpClientFactory clientFactory)
        {
            _parser = parser;
            _planner = planner;
            _clientFactory = clientFactory;
        }


        [HttpGet]
        [Route("travelPlan")]
        public async Task<IActionResult> GetRoute([FromQuery] string from, string to, string start)
        {
            if(_busRoutes == null || _busRoutes.Count == 0)
            {
                var client = _clientFactory.CreateClient("travelPlanner");
                var travelPlanResponse = (await client.GetAsync("")).EnsureSuccessStatusCode();
                var responseBody = await travelPlanResponse.Content.ReadAsStringAsync();
                _busRoutes = _parser.ParseJson(responseBody);
            }
            var result = _planner.GetNextRoute(_busRoutes, from, to, start);

            if (result == null)
                return NotFound();
            
            return Ok(result);
        }
    }
}
