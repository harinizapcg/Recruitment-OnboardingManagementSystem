using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

namespace ROMSS.UI.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public RolesController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        // =========================
        // 🔐 COMMON CLIENT METHOD
        // =========================
        private HttpClient GetClient()
        {
            var client = _factory.CreateClient("ApiClient");

            var token = HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        // =========================
        // 📋 LIST ROLES
        // =========================
        public async Task<IActionResult> Index()
        {
            var client = GetClient();

            var response = await client.GetAsync("Roles");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = await response
                .Content
                .ReadFromJsonAsync<List<RoleDto>>();

            return View(data);
        }

        // =========================
        // ➕ ADD ROLE (GET)
        // =========================
        public IActionResult Add()
        {
            return View();
        }

        // =========================
        // ➕ ADD ROLE (POST)
        // =========================
        [HttpPost]
        public async Task<IActionResult> Add(AddRoleRequestDto model)
        {
            var client = GetClient();

            var response = await client
                .PostAsJsonAsync("Roles", model);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to add role.";

                return View(model);
            }

            return RedirectToAction("Index");
        }

        // =========================
        // ✏️ EDIT ROLE (GET)
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetClient();

            var response = await client.GetAsync($"Roles/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var data = await response
                .Content
                .ReadFromJsonAsync<UpdateRoleRequestDto>();

            return View(data);
        }

        // =========================
        // ✏️ EDIT ROLE (POST)
        // =========================
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoleRequestDto model)
        {
            var client = GetClient();

            var response = await client
                .PutAsJsonAsync($"Roles/{model.RoleId}", model);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update role.";

                return View(model);
            }

            return RedirectToAction("Index");
        }

        // =========================
        // 🗑️ DELETE ROLE
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetClient();

            var response = await client.DeleteAsync($"Roles/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to delete role.";
            }

            return RedirectToAction("Index");
        }
    }
}