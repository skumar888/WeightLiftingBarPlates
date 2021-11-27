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
        private const decimal equipmentWeight = 45;
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
        public ActionResult GetEquipWeight()
        {
            return Ok(equipmentWeight);
        }

        // POST: api/WLB/
        [HttpPost]
        public async Task<ActionResult<Object>> GetMinimumPlates([FromBody]string inputString) 
        {
            _loggerManager.LogInfo($"Request recieved for min weight list :{inputString}");

            if (inputString == null)
            {
                _loggerManager.LogError($"Input String missing");
                return BadRequest();
            }

            var WLBMaximumAllowedWeightIndexes = _configuration.GetValue<decimal>("WLBMaximumAllowedWeightIndexes");

            var availaiblePlates = await _platesRepository.GetAllPlates();
            var precision = _inputValidatorAndParser.GetPrecision(availaiblePlates.ToList().Select(x=>x.weight).ToArray());//gets GCD of available weight plates, which will be used to verify input weights and cal min plates
            

            var inputWeighrList = _inputValidatorAndParser.ValidateAndParseWeight(inputString, WLBMaximumAllowedWeightIndexes, availaiblePlates.ToList(), equipmentWeight, precision);
            var result=   _getMinimumPlates.GetMinimumPairedPlatesForWeights(inputWeighrList, equipmentWeight, precision, availaiblePlates.ToList());
            return  Ok(_jsonParser.SerializeObjects(result));

        }
    }
}
