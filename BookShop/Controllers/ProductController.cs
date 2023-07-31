using BookShop.Data.Interfaces;
using BookShop.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _env = env;
        }
        // GET: ProductController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productRepository.GetAllProductsAsync();            
            return View(products);
        }

        // GET: ProductController/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> CategoryList = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.CategoryList = CategoryList;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Generate a unique filename for the uploaded image
                        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                        // Get the physical path to the uploads directory
                        string uploadsDir = Path.Combine(_env.WebRootPath, "uploads");

                        // Create the uploads directory if it doesn't exist
                        if (!Directory.Exists(uploadsDir))
                        {
                            Directory.CreateDirectory(uploadsDir);
                        }

                        // Combine the uploads directory and the unique filename to get the full path
                        string filePath = Path.Combine(uploadsDir, uniqueFileName);

                        // Save the image to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // Set the ImagePath property of the Product to the relative path of the uploaded image
                        product.Image = "/uploads/" + uniqueFileName;
                    }
                    int result = await _productRepository.InsertProductAsync(product);
                    if (result > 0)
                        TempData["success"] = $"Category {product.Title} Added Successfully";
                    return RedirectToAction(nameof(Index));
                //}
                //return View(product);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productRepository.GetProductByIdAsync(id);
            IEnumerable<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> CategoryList = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == product.CategoryId
            }).ToList();

            ViewBag.CategoryList = CategoryList;
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = await _productRepository.UpdateProductAsync(id, product);
                    if (IsUpdated)
                        TempData["success"] = "Category Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool IsDeleted = await _productRepository.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
