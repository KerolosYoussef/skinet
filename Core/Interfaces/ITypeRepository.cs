using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITypeRepository
    {
        Task<ProductType> GetProductTypeByIdAsync(int id);

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}