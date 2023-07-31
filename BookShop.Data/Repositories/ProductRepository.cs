using BookShop.Data.Contexts;
using BookShop.Data.Interfaces;
using BookShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> products = await _db.Products.Include(p => p.Category).ToListAsync();
            return products;
        }
        public async Task<int> InsertProductAsync(Product product)
        {
            try
            {
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
                int id = await _db.Products.MaxAsync(s => s.Id);
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<bool> UpdateProductAsync(int productId, Product product)
        {
            var data = await _db.Products.FirstOrDefaultAsync(s => s.Id == productId);

            if (data == null) return false;

            data.Title = product.Title;
            data.Description = product.Description;
            data.ISBN = product.ISBN;
            data.Author = product.Author;
            data.ListPrice = product.ListPrice;
            data.Price = product.Price;
            data.Price50 = product.Price50;
            data.Price100 = product.Price100;
            data.CategoryId = product.CategoryId;
            _db.Products.Update(data);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            Product? data = await _db.Products.FirstOrDefaultAsync(s => s.Id == productId);
            return data!;
        }
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var data = await _db.Products.FirstOrDefaultAsync(s => s.Id == productId);
            if (data == null) return false;
            _db.Remove(data);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
