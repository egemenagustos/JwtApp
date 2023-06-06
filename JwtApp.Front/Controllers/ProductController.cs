using FluentValidation;
using JwtApp.Front.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace JwtApp.Front.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IValidator<UpdateProductModel> _updateValidator;
        private readonly IValidator<CreateProductModel> _createValidtor;

        public ProductController(IHttpClientFactory httpClientFactory, IValidator<UpdateProductModel> updateValidator, IValidator<CreateProductModel> createValidtor)
        {
            _httpClientFactory = httpClientFactory;
            _updateValidator = updateValidator;
            _createValidtor = createValidtor;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://localhost:8080/api/Products");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<ProductListModel>>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    return View(result);
                }
            }

            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.DeleteAsync($"http://localhost:8080/api/Products/{id}");
            }

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://localhost:8080/api/Categories");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    var model = new CreateProductModel
                    {
                        Categories = new SelectList(data, "Id", "Definition")
                    };
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductModel request)
        {
            var data = TempData["Categories"]?.ToString();
            if (data != null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                request.Categories = new SelectList(categories, "Value", "Text");
            }

            var result = _createValidtor.Validate(request);

            if (result.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var jsonData = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var responseProduct = await client.PostAsync("http://localhost:8080/api/Products", content);

                    if (responseProduct.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    ModelState.AddModelError("", "Bir hata oluştu!");
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseProducts = await client.GetAsync($"http://localhost:8080/api/Products/{id}");

                if (responseProducts.IsSuccessStatusCode)
                {
                    var jsonData = await responseProducts.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UpdateProductModel>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    var responseCategory = await client.GetAsync("http://localhost:8080/api/Categories");

                    if (responseCategory.IsSuccessStatusCode)
                    {
                        var jsonCategoryData = await responseCategory.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonCategoryData, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                        if (result != null)
                        {
                            result.Categories = new SelectList(data, "Id", "Definition");
                        }
                    }
                    return View(result);
                }
            }
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductModel request)
        {
            var data = TempData["Categories"]?.ToString();
            if (data != null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                request.Categories = new SelectList(categories, "Value", "Text", request.CategoryId);
            }

            var result = _updateValidator.Validate(request);

            if (result.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var jsonData = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var responseProduct = await client.PutAsync("http://localhost:8080/api/Products", content);

                    if (responseProduct.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    ModelState.AddModelError("", "Bir hata oluştu!");
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(request);
        }

    }
}
