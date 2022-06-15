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
    public class TypesController : ControllerBase
    {
        private readonly IGenericRepository<ProductType> _repo;
        public TypesController(IGenericRepository<ProductType> repo)
        {
            _repo = repo;
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetType(int id){
            var type = await _repo.GetByIdAsync(id);
            if(type == null)
                return NotFound();

            return type;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductType>>> GetTypes(){
            var types = await _repo.ListAllAsync();
        
            return Ok(types);
        }
    }
}