using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using API.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        
        private readonly IGenericRepository<Product> _repo;
        public IMapper _mapper { get; }

        public ProductsController(IGenericRepository<Product> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }  

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(){
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _repo.ListAsync(spec);
            
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id){

            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product = await _repo.GetEntityWithSpec(spec);
            if(product == null)
                return NotFound();
            
            return _mapper.Map<Product,ProductDto>(product);
        }
    }
}