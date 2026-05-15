using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

public class UsersController : Controller
{
    private readonly IHttpClientFactory _factory;

    public UsersController(IHttpClientFactory factory)
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
        var data = await client.GetFromJsonAsync<List<UserDto>>("Users");
        return View(data);
    }

    // ➕ ADD (GET)
    public async Task<IActionResult> Add()
    {
        await LoadRoles();
        return View();
    }

    // ➕ ADD (POST)
    [HttpPost]
    public async Task<IActionResult> Add(AddUserRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Users", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to add user.";
            await LoadRoles();
            return View(model);
        }
        return RedirectToAction("Index");
    }

    // ✏️ EDIT (GET)
    public async Task<IActionResult> Edit(int id)
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<UpdateUserRequestDto>($"Users/{id}");
        if (data == null) return NotFound();
        await LoadRoles();
        return View(data);
    }

    // ✏️ EDIT (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateUserRequestDto model)
    {
        var client = GetClient();
        var response = await client.PutAsJsonAsync($"Users/{model.UserId}", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to update user.";
            await LoadRoles();
            return View(model);
        }
        return RedirectToAction("Index");
    }

    // 🗑️ DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var client = GetClient();
        await client.DeleteAsync($"Users/{id}");
        return RedirectToAction("Index");
    }

    // 🔧 HELPER — loads roles into ViewBag for dropdown
    private async Task LoadRoles()
    {
        var client = GetClient();
        var roles = await client.GetFromJsonAsync<List<RoleDto>>("Roles");
        ViewBag.Roles = roles ?? new List<RoleDto>();
    }
}