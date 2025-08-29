using System.Threading.Tasks;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index([FromQuery] ProductRequestParameters p)
        {
            ViewData["Title"] = "Products";

            var products = _manager.ProductService.GetAllProductsWithDetails(p,false);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_manager.CategoryService.GetAllCategories(false), "CategoryId", "CategoryName", "1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDtoForInsertion, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDtoForInsertion.ImageUrl = string.Concat("/images/", file.FileName);
                _manager.ProductService.CraeteProduct(productDtoForInsertion);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            var prd = _manager.ProductService.GetOneProductForUpdate(id, true);
            ViewBag.Categories = new SelectList(_manager.CategoryService.GetAllCategories(false), "CategoryId", "CategoryName", "{prd.CategoryId.ToString()}");

            return View(prd);
        }

       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile? file)
{
    if (ModelState.IsValid)
    {
        if (file != null && file.Length > 0) // yeni resim seçilmişse
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            productDto.ImageUrl = string.Concat("/images/", file.FileName);
        }
        else
        {
            // Yeni resim seçilmemişse eski resim URL'sini koru
            var existingProduct = _manager.ProductService.GetOneProduct(productDto.ProductId,false);
            productDto.ImageUrl = existingProduct.ImageUrl;
        }

        _manager.ProductService.UpdateProductDtoForUpdate(productDto);
        return RedirectToAction("Index");
    }

    return View(productDto);
}

        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            TempData["danger"] = "Product is deleted.";
            return RedirectToAction("Index");
        }

    }
}