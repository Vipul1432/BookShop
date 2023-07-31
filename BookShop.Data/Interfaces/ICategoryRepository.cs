using BookShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data.Interfaces
{
	public interface ICategoryRepository
	{
		/// <summary>
		/// It return all the category
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Category>> GetAllCategoriesAsync();
		/// <summary>
		/// It Insert the category in database
		/// </summary>
		/// <param name="category"></param>
		/// <returns>created id if database inserted else 0</returns>
		Task<int> InsertCategoryAsync(Category category);
		/// <summary>
		/// It Update based on id in database
		/// </summary>
		/// <param name="categoryId"></param>
		/// <param name="category"></param>
		/// <returns></returns>
		Task<bool> UpdateCategoryAsync(int categoryId, Category category);
		/// <summary>
		/// It return Category by it's id 
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		Task<Category> GetCategoryByIdAsync(int categoryId);
		/// <summary>
		/// It delete the record based on user id from databse
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
