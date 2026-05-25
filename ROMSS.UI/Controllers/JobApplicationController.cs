using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

public class JobApplicationController : Controller
{
    private readonly IHttpClientFactory _factory;

    public JobApplicationController(IHttpClientFactory factory)
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

    public async Task<IActionResult> Index(int jobId)
    {
        var client = GetClient();
        var response = await client.GetAsync($"JobApplication/job/{jobId}");
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to load applications.";
            ViewBag.JobId = jobId;
            return View(new List<JobApplicationDto>());
        }
        var data = await response.Content.ReadFromJsonAsync<List<JobApplicationDto>>();
        ViewBag.JobId = jobId;
        return View(data);
    }

    public IActionResult Apply(int jobId)
    {
        ViewBag.JobId = jobId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Apply(ApplyJobRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("JobApplication/apply", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to apply.";
            ViewBag.JobId = model.JobId;
            return View(model);
        }
        return RedirectToAction("Index", new { jobId = model.JobId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Shortlist(int id, int jobId)
    {
        var client = GetClient();
        await client.PutAsync($"JobApplication/shortlist/{id}", null);
        return RedirectToAction("Index", new { jobId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int id, int jobId)
    {
        var client = GetClient();
        await client.PutAsync($"JobApplication/reject/{id}", null);
        return RedirectToAction("Index", new { jobId });
    }
}