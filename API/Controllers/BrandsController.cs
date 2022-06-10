using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _repo;

        public BrandsController(IBrandRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetBrand(int id){
            var productBrand = await _repo.GetProductBrandByIdAsync(id);
            
            if(productBrand == null)
                return NotFound();

            return Ok(productBrand);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands(){
            var productBrands = await _repo.GetProductBrandsAsync();
            
            return Ok(productBrands);
        }
    }
}