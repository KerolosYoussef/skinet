using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using API.Dtos;
using API.Errors;
using API.Helpers;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
    
        private readonly IGenericRepository<Product> _repo;
        public IMapper _mapper { get; }

        public ProductsController(IGenericRepository<Product> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }  

        [HttpGet]
        [EnableCors("CorsPolicy")] 
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productParamas){

            var spec = new ProductsWithTypesAndBrandsSpecification(productParamas);

            var countSpec = new ProductsWithFiltersForCountSpecification(productParamas);

            var totalItems = await _repo.CountAsync(countSpec);

            var products = await _repo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products);
            
            return Ok(new Pagination<ProductDto>(productParamas.PageIndex, productParamas.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id){

            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product = await _repo.GetEntityWithSpec(spec);
            if(product == null)
                return NotFound(new ApiResponse(404));
            
            return _mapper.Map<Product,ProductDto>(product);
        }
        
    }
}