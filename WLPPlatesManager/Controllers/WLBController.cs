using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WLBApplication.Application;
using WLBApplication.Model;
using WLPBlatesManager.Model;

namespace WLBPlatesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WLBController : ControllerBase
    {
        private const decimal weight = 45;
        private IJsonParser _jsonParser;
        private IInputValidatorAndParser _inputValidatorAndParser;
        private IGetMinimumPlates _getMinimumPlates;
        IConfiguration _configuration;

        public WLBController(IJsonParser jsonParser, IInputValidatorAndParser inputValidatorAndParser, IGetMinimumPlates getMinimumPlates, IConfiguration configuration)
        {
            _jsonParser = jsonParser;
            _inputValidatorAndParser = inputValidatorAndParser;
            _getMinimumPlates = getMinimumPlates;
            _configuration = configuration;
        }

        // GET: api/WLB
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(weight);
        }

        // GET: api/WLB/5
        [HttpGet("{inputString}")]
        public ActionResult<IEnumerable<WLBMinResult>> Get(string inputString) //ActionResult<IEnumerable< WLBMinResult>>
        {
            var precision = _configuration.GetValue<decimal>("WLBPlatesWeightPrecision");
            var maximumAllowedWeight = _configuration.GetValue<decimal>("WLBMaximumAllowedWeight");
            var inputArray = _inputValidatorAndParser.ValidateAndParseWeight(inputString, precision, maximumAllowedWeight);
            return   _getMinimumPlates.GetMinimumPairedPlatesForWeights(inputArray, weight, precision);
            //return  _jsonParser.SerializeObjects(result);

        }

        // POST: api/WLB
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
