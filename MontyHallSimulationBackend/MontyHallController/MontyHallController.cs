using Microsoft.AspNetCore.Mvc;

namespace MontyHallApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyHallController : ControllerBase
    {
        [HttpGet("simulate")]
        public IActionResult Simulate(int numberOfSimulations, bool switchDoor)
        {
            var results = new MontyHallSimulation().Run(numberOfSimulations, switchDoor);
            return Ok(results);
        }
    }

    public class MontyHallSimulation
    {
        public SimulationResult Run(int numberOfSimulations, bool switchDoor)
        {
            int wins = 0;
            Random random = new Random();

            for (int i = 0; i < numberOfSimulations; i++)
            {
                int carDoor = random.Next(3);
                int playerChoice = random.Next(3);

                int hostChoice;
                do
                {
                    hostChoice = random.Next(3);
                } while (hostChoice == carDoor || hostChoice == playerChoice);

                if (switchDoor)
                {
                    playerChoice = 3 - playerChoice - hostChoice;
                }

                if (playerChoice == carDoor)
                {
                    wins++;
                }
            }

            return new SimulationResult
            {
                NumberOfSimulations = numberOfSimulations,
                Wins = wins,
                Losses = numberOfSimulations - wins
            };
        }
    }

    public class SimulationResult
    {
        public int NumberOfSimulations { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}
