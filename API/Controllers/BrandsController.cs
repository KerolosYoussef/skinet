using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BrandsController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _repo;

        public BrandsController(IGenericRepository<ProductBrand> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetBrand(int id){
            var productBrand = await _repo.GetByIdAsync(id);
            
            if(productBrand == null)
                return NotFound();

            return Ok(productBrand);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands(){
            var productBrands = await _repo.ListAllAsync();
            
            return Ok(productBrands);
        }
    }
}