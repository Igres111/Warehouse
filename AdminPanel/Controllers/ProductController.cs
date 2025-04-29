using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7165");
        }

        public async Task<IActionResult> Index()
        {

            var products = await _httpClient.GetFromJsonAsync<List<ProductViewModel>>("/api/Product/Get-All-Products");
            return View(products);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductViewModel>($"api/Product/Get-Product-By-Id/{id}");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Product/Update-Product", product);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
            return View(product);
        }
    }
}
