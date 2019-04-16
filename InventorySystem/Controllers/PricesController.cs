﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySystem.DataLayerClasses;
using InventorySystem.Models;
using Microsoft.AspNet.OData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventorySystem.Controllers
{
    [Route("api/[controller]")]
    public class PricesController : Controller
    {
        private readonly IConfiguration _configuration;

        public PricesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/prices?id
        [HttpGet("{id?}")]
        [EnableQuery()]
        public IEnumerable<Price> Get(int? id)
        {
            return new PricesDataLayer(_configuration).GetPrice(id);
        }

        // POST api/prices
        [HttpPost]
        //[Consumes("application/json")]
        public IActionResult Post([FromBody] Price price)
        {
            return Ok(new PricesDataLayer(_configuration).InsertPrice(price));
        }

        // PUT api/prices/1
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Price price)
        {
            return Ok(new PricesDataLayer(_configuration).UpdatePrice(price));
        }

        // DELETE api/prices/1
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] Price price)
        {
            return Ok(new PricesDataLayer(_configuration).DeletePrice(price));
        }
    }
}
