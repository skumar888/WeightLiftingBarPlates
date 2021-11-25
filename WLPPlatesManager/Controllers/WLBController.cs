using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WLBApplication.Application;
using WLBApplication.Model;
using WLBLoggingService;
using WLPBlatesManager.Model;

namespace WLBPlatesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WLBController : ControllerBase
    {
        private const decimal weight = 45; //toDo: update the name to equiweight
        private IJsonParser _jsonParser;
        private IInputValidatorAndParser _inputValidatorAndParser;
        private IGetMinimumPlates _getMinimumPlates;
        private IConfiguration _configuration;
        private ILoggerManager _loggerManager;
        private readonly IPlatesRepository _platesRepository;

        public WLBController(IJsonParser jsonParser, IInputValidatorAndParser inputValidatorAndParser, IGetMinimumPlates getMinimumPlates, IConfiguration configuration, ILoggerManager loggerManager ,IPlatesRepository platesRepository)
        {
            _jsonParser = jsonParser;
            _inputValidatorAndParser = inputValidatorAndParser;
            _getMinimumPlates = getMinimumPlates;
            _configuration = configuration;
            _loggerManager = loggerManager;
            _platesRepository = platesRepository;
        }

        // GET: api/WLB
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(weight);
        }

        // GET: api/WLB/5
        [HttpGet("{inputString}")]
        public async Task<ActionResult<Object>> Get(string inputString) //ActionResult<IEnumerable< WLBMinResult>>
        {
            _loggerManager.LogInfo($"Request recieved for min weight list :{inputString}");

            var availaiblePlates = await _platesRepository.GetAllPlates();
            var precision = _inputValidatorAndParser.GetPricision(availaiblePlates.ToList().Select(x=>x.weight).ToArray());
            //var precision = _configuration.GetValue<decimal>("WLBPlatesWeightPrecision");
            var maximumAllowedWeight = _configuration.GetValue<decimal>("WLBMaximumAllowedWeight");

            var inputWeighrList = _inputValidatorAndParser.ValidateAndParseWeight(inputString, maximumAllowedWeight, availaiblePlates.ToList(), weight,precision);
            var result=   _getMinimumPlates.GetMinimumPairedPlatesForWeights(inputWeighrList, weight, precision, availaiblePlates.ToList());
            return  Ok(_jsonParser.SerializeObjects(result));

        }

        // POST: api/WLB
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //ToDO:implement min list in post
            //remember 

        }

        // PUT: api/WLB/5
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
