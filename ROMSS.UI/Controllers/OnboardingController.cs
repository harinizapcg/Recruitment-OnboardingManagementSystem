using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

public class OnboardingController : Controller
{
    private readonly IHttpClientFactory _factory;

    public OnboardingController(IHttpClientFactory factory)
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
        var response = await client.GetAsync($"Onboarding/{applicationId}");

        OnboardingDto? data = null;

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(content) && content != "null")
                data = System.Text.Json.JsonSerializer.Deserialize<OnboardingDto>(content,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }

        ViewBag.ApplicationId = applicationId;
        return View(data);
    }

    public IActionResult Upload(int applicationId)
    {
        ViewBag.ApplicationId = applicationId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(UploadDocumentsRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Onboarding/upload-docs", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to upload documents.";
            ViewBag.ApplicationId = model.ApplicationId;
            return View(model);
        }
        return RedirectToAction("Index", new { applicationId = model.ApplicationId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Verify(int applicationId)
    {
        var client = GetClient();
        await client.PutAsJsonAsync($"Onboarding/verify/{applicationId}", new { });
        return RedirectToAction("Index", new { applicationId });
    }
}