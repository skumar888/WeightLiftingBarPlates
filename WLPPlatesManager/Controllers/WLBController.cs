using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WLBApplication.Application;
using WLBApplication.Model;
using WLPBlatesManager.Model;

namespace WLBPlatesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WLBController : ControllerBase
    {
        private const double weight = 45;
        private IJsonParser _jsonParser;
        private IInputValidatorAndParser _inputValidatorAndParser;
        private IGetMinimumPlates _getMinimumPlates;

        public WLBController(IJsonParser jsonParser, IInputValidatorAndParser inputValidatorAndParser, IGetMinimumPlates getMinimumPlates)
        {
            _jsonParser = jsonParser;
            _inputValidatorAndParser = inputValidatorAndParser;
            _getMinimumPlates = getMinimumPlates;
        }

        // GET: api/WLB
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(weight);
        }

        // GET: api/WLB/5
        [HttpGet("{inputString}")]
        public string Get(string inputString)
        {
            var inputArray = _inputValidatorAndParser.ValidateAndParseWeight(inputString);
            var result = _getMinimumPlates.GetMinimumPairedPlatesForWeights(inputArray, weight);
            return  _jsonParser.SerializeObject(result);

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
