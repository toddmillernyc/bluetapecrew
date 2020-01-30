﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> Find(int id);
        Task Update(Category category);
        Task<IEnumerable<Category>> GetAll();
        Task Create(Category category);
        Task<List<Category>> GetAllWithProducts();
        Task<List<Category>> GetAllPublishedWithProducts();
        Task<IEnumerable<Category>> GetAllPublishedWithProductsAndStyles();
        Task Delete(int id);
    }
}
