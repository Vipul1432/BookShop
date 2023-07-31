using BookShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
		/// It return all the Products
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Product>> GetAllProductsAsync();
        /// <summary>
        /// It Insert the category in database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>created id if database inserted else 0</returns>
        Task<int> InsertProductAsync(Product product);
        /// <summary>
        /// It Update product based on id in database
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<bool> UpdateProductAsync(int productId, Product product);
        /// <summary>
        /// It return Category by it's id 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<Product> GetProductByIdAsync(int productId);
        /// <summary>
        /// It delete the record based on user id from databse
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(int productId);
    }
}
