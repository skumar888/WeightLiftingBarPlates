﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WLPBlatesManager.Model;
using WLBApplication.Application;

namespace WLPBlatesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatesController : ControllerBase
    {

        private readonly IPlatesRepository _platesRepository;
        private readonly IJsonParser _jsonParser;

        public PlatesController(IPlatesRepository platesRepository, IJsonParser jsonParser)
        {

            _platesRepository = platesRepository;
            _jsonParser = jsonParser;
        }

        // GET: api/Plates
        [HttpGet]
        public async Task<ActionResult>  Get()
        {
            var result = await _platesRepository.GetAllPlates();
            return Ok(_jsonParser.SerializeObject(result));
        }

        // GET: api/Plates/5
        [HttpGet("{weight}")]
        public async Task<ActionResult<Plate>> Get(double weight)
        {
            var result = await _platesRepository.GetPlate(weight);
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
