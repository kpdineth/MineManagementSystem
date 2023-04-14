using Microsoft.AspNetCore.Mvc;
using MineSystems.Models;

namespace MineSystems.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public IActionResult Post()
        {
            var data = GenerateWeatherForecasts();
            return Ok(data);
        }

        private IEnumerable<WeatherForecast> GenerateWeatherForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost("PostNumber")]
        public IActionResult PostNumber([FromBody] int number)
        {
            _logger.LogInformation($"Received numeric value: {number}");

            // Process the numeric value here (e.g., save to a database, etc.)

            return Ok("Numeric value processed successfully.");
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("api/[controller]/[action]")]
        //[HttpPost(Name = "PrePostPersonData")]
        public IActionResult PrePostPersonData([FromBody] Person Terson)
        {
            try
            {
                if (Terson == null || string.IsNullOrEmpty(Terson.Name) || Terson.Age <= 0)
                {
                    return BadRequest("Invalid person data.");
                }

                _logger.LogInformation($"Received person data: Name = {Terson.Name}, Age = {Terson.Age}");

                // Process the person data here (e.g., save to a database, etc.)

                return Ok("Person data processed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing person data: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult PostPersonData([FromBody] Person person)
        {
            if (person == null || string.IsNullOrEmpty(person.Name) || person.Age <= 0)
            {
                return BadRequest("Invalid person data.");
            }

            _logger.LogInformation($"Received person data: Name = {person.Name}, Age = {person.Age}");

            // Process the person data here (e.g., save to a database, etc.)

            return Ok("Person data processed successfully.");
        }

    }



}