using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Library;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class TravelPlannerController : ControllerBase
    {
        private static List<BusRoute> _BusRoutes;
        private readonly ITravelPlanner parser;
        private readonly ITravelPlanParser planner;

        public TravelPlannerController(ITravelPlanner parser, ITravelPlanParser planner)
        {
            this.parser = parser;
            this.planner = planner;
        }


        [HttpGet]
        [Route("travelPlan")]
        public async Task<IActionResult> GetRoute([FromQuery] string from, string to, string start)
        {
            if(_BusRoutes == null || _BusRoutes.Count == 0)
            {
                var text = await ReadTextFile("travelPlan.json");
                TravelPlanParser parser = new TravelPlanParser();
                _BusRoutes = parser.ParseJson(text);
            }

            TravelPlanner planner = new TravelPlanner();
            var result = planner.GetNextRoute(_BusRoutes, from, to, start);

            if (result == null)
                return NotFound();
            
            return Ok(result);
        }


        public static async Task<string> ReadTextFile(string filename)
        {
            string text;
            try
            {
                text = await System.IO.File.ReadAllTextAsync(filename);
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine("Json file not found!\n" + ex.ToString());
                throw;
            }

            return text;
        }
    }

}
