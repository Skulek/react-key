using System;
using System.Collections.Generic;
using System.Linq;
using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("myPolicy")]
    public class KeyValuePairController : ControllerBase
    {
        private readonly IKeyValuePairService keyValuePairService;

        public KeyValuePairController(IKeyValuePairService keyValuePairService)
        {
            this.keyValuePairService = keyValuePairService;
        }

        [HttpGet]
        public IEnumerable<KeyValuePairDto> Get()
        {
            return keyValuePairService.GetKeyValuePairs();
        }

        [HttpPost]
        public IActionResult PostData(KeyValuePairDto value)
        { 
            var status = keyValuePairService.InsertKeyValuePair(value);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
