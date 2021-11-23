using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WLPBlatesManager.Model;
using WLBApplication.Application;
using WLBLoggingService;

namespace WLPBlatesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatesController : ControllerBase
    {

        private readonly IPlatesRepository _platesRepository;
        private readonly IJsonParser _jsonParser;
        private readonly ILoggerManager _logManager;

        public PlatesController(IPlatesRepository platesRepository, IJsonParser jsonParser, ILoggerManager logManager)
        {

            _platesRepository = platesRepository;
            _jsonParser = jsonParser;
            _logManager = logManager;
        }

        // GET: api/Plates
        [HttpGet]
        public async Task<ActionResult>  Get()
        {
            _logManager.LogInfo("Request recieved for all plates");
            var result = await _platesRepository.GetAllPlates();

            _logManager.LogInfo($"Response sent{_jsonParser.SerializeObject(result)}");
            return Ok(_jsonParser.SerializeObjects(result));
        }

        // GET: api/Plates/5
        [HttpGet("{weight}")]
        public async Task<ActionResult<Plate>> Get(decimal weight)
        {
            _logManager.LogInfo($"Request recieved for plate {weight}");
            var result = await _platesRepository.GetPlate(weight);

            _logManager.LogInfo($"Response sent{_jsonParser.SerializeObject(result)}");
            return Ok( _jsonParser.SerializeObject(result));
        }

        // POST: api/Plates
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Plates/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
