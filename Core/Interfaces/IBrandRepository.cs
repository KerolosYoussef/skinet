using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBrandRepository
    {
        Task<ProductBrand> GetProductBrandByIdAsync(int id);

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }
}