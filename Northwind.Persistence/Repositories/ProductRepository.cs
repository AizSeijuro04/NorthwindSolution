﻿using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Base;
using Northwind.Domain.Dto;
using Northwind.Domain.Models;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Product product)
        {
            Update(product);
        }

        public async Task<IEnumerable<Product>> GetAllProduct(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.ProductId)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .ToListAsync();
        }


        public async Task<Product> GetProductById(int productId, bool trackChanges)
        {
            return await FindByCondition(p => p.ProductId.Equals(productId), trackChanges)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductOnSales(bool trackChanges)
        {
            /*select * from products p 
             * where exists(select * from ProductPhotos pp 
             * where pp.PhotoProductId = p.ProductID)*/

            // diatas query SQL dibawah query by c#

            var product = await FindAll(trackChanges)
                .Where(x => x.ProductPhotos.Any(y => y.PhotoProductId == x.ProductId))
                .Include(p => p.ProductPhotos)
                .ToListAsync();
            return product;
        }

        public async Task<Product> GetProductOnSalesById(int productId, bool trackChanges)
        {
            var products = await FindByCondition(x => x.ProductId.Equals(productId), trackChanges)
                .Where(y => y.ProductPhotos.Any(p => p.PhotoProductId == productId))
                .Include(a => a.ProductPhotos)
                .Include(b => b.Category)
                .SingleOrDefaultAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(p => p.ProductId)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public IEnumerable<TotalProductByCategory> GetTotalProductByCategory()
        {
            var rawSQL = _dbContext.TotalProductByCategorySQL
                .FromSqlRaw("select c.CategoryName,COUNT(p.productID) as totalproduct " +
                "from products p join Categories c on p.categoryID=c.categoryID " +
                "group by c.categoryname")
                .Select(x => new TotalProductByCategory
                {
                    CategoryName = x.CategoryName,
                    TotalProduct = x.TotalProduct
                })
                .OrderBy(x => x.TotalProduct)
                .ToList();
            return rawSQL;
        }

        public void Insert(Product product)
        {
            Create(product);
        }

        public void Remove(Product product)
        {
            Delete(product);
        }
    }
}
