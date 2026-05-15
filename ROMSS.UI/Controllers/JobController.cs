using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

public class JobController : Controller
{
    private readonly IHttpClientFactory _factory;

    public JobController(IHttpClientFactory factory)
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

    // 📋 LIST
    public async Task<IActionResult> Index()
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<List<JobDto>>("Job");
        return View(data);
    }

    // ➕ ADD (GET)
    public IActionResult Add() => View();

    // ➕ ADD (POST)
    [HttpPost]
    public async Task<IActionResult> Add(AddJobRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Job", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to add job.";
            return View(model);
        }
        return RedirectToAction("Index");
    }

    // ✏️ EDIT (GET)
    public async Task<IActionResult> Edit(int id)
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<UpdateJobRequestDto>($"Job/{id}");
        if (data == null) return NotFound();
        return View(data);
    }

    // ✏️ EDIT (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateJobRequestDto model)
    {
        var client = GetClient();
        var response = await client.PutAsJsonAsync($"Job/{model.Id}", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to update job.";
            return View(model);
        }
        return RedirectToAction("Index");
    }

    // 🗑️ DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var client = GetClient();
        await client.DeleteAsync($"Job/{id}");
        return RedirectToAction("Index");
    }
}