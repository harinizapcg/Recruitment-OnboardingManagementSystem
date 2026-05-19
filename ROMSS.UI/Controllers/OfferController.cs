using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ROMSS.UI.Controllers
{
    public class OfferController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public OfferController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

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
        // VIEW OFFER
        // =========================
        public async Task<IActionResult> Index(int applicationId)
        {
            var client = GetClient();

            var response = await client.GetAsync($"Offer/{applicationId}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Offer not found";
                return View(null);
            }

            var data = await response.Content.ReadFromJsonAsync<OfferDto>();

            return View(data);
        }

        // =========================
        // GENERATE OFFER (GET)
        // =========================
        public IActionResult Generate()
        {
            return View();
        }

        // =========================
        // GENERATE OFFER (POST)
        // =========================
        [HttpPost]
        public async Task<IActionResult> Generate(GenerateOfferRequestDto model)
        {
            var client = GetClient();

            var response = await client.PostAsJsonAsync("Offer/generate", model);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to generate offer";
                return View(model);
            }

            return RedirectToAction("Index", new { applicationId = model.ApplicationId });
        }
    }
}