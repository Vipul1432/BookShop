using BookShop.Data.Interfaces;
using BookShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: CategoryController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: CategoryController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = await _categoryRepository.InsertCategoryAsync(category);
                    if (result > 0)
                        TempData["success"] = $"Category {category.Name} Added Successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _categoryRepository.GetCategoryByIdAsync(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = await _categoryRepository.UpdateCategoryAsync(id, category);
                    if (IsUpdated)
                        TempData["success"] = "Category Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool IsDeleted = await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }

    }
}
