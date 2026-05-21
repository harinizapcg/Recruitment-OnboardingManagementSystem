using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

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
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    public async Task<IActionResult> Index(int applicationId)
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<OfferDto>($"Offer/{applicationId}");
        ViewBag.ApplicationId = applicationId;
        return View(data);
    }

    public IActionResult Generate(int applicationId)
    {
        ViewBag.ApplicationId = applicationId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Generate(GenerateOfferRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Offer/generate", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to generate offer.";
            return View(model);
        }
        return RedirectToAction("Index", new { applicationId = model.ApplicationId });
    }
}