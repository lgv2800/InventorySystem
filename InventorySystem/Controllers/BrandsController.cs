﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InventorySystem.DataLayerClasses;
using InventorySystem.Models;
using System.IO;
using Microsoft.AspNet.OData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventorySystem.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        private readonly IConfiguration _configuration;

        public BrandsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/brands?id
        [HttpGet("{id?}")]
        [EnableQuery()]
        public IEnumerable<Brand> Get(int? id)
        {
            return new BrandsDataLayer(_configuration).GetBrand(id);
        }

        // POST api/brand
        [HttpPost]
        //[Consumes("application/json")]
        public IActionResult Post([FromBody] Brand brand)
        {
            return Ok(new BrandsDataLayer(_configuration).InsertBrand(brand));
        }

        // PUT api/brand/1
        [HttpPut]
        public IActionResult Put([FromBody] BrandsUpdate brandsUpdate)
        {
            return Ok(new BrandsDataLayer(_configuration).UpdateBrand(brandsUpdate));
        }

        // DELETE api/brand/1
        [HttpDelete]
        public IActionResult Delete([FromBody] BrandsDelete brandsDelete)
        {
            return Delete(new BrandsDataLayer(_configuration).DeleteBrand(brandsDelete));
        }
    }
}
