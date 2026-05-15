using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

public class RolesController : Controller
{
    private readonly IHttpClientFactory _factory;

    public RolesController(IHttpClientFactory factory)
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
        var data = await client.GetFromJsonAsync<List<RoleDto>>("Roles");
        return View(data);
    }

    // ➕ ADD (GET)
    public IActionResult Add() => View();

    // ➕ ADD (POST)
    [HttpPost]
    public async Task<IActionResult> Add(AddRoleRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Roles", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to add role.";
            return View(model);
        }
        return RedirectToAction("Index");
    }

    // ✏️ EDIT (GET)
    public async Task<IActionResult> Edit(int id)
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<UpdateRoleRequestDto>($"Roles/{id}");
        if (data == null) return NotFound();
        return View(data);
    }

    // ✏️ EDIT (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateRoleRequestDto model)
    {
        var client = GetClient();
        var response = await client.PutAsJsonAsync($"Roles/{model.RoleId}", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to update role.";
            return View(model);
        }
        return RedirectToAction("Index");
    }

    // 🗑️ DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var client = GetClient();
        await client.DeleteAsync($"Roles/{id}");
        return RedirectToAction("Index");
    }
}