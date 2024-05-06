using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory> AddAsync(ProductCategory productCategory);
    }
}
