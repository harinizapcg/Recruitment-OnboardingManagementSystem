using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;

public class AuthController : Controller
{
    private readonly IHttpClientFactory _factory;

    public AuthController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    // =========================
    // 🔐 LOGIN
    // =========================

    // GET: /Auth/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Auth/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto model)
    {
        var client = _factory.CreateClient("ApiClient");

        var response = await client.PostAsJsonAsync("Auth/login", model);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Invalid credentials";
            return View(model);
        }
        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

        // ✅ Store JWT
        HttpContext.Session.SetString("JWT", result.Token);

        // 🔍 DEBUG (check in console)
        Console.WriteLine("TOKEN SAVED: " + result.Token);

        return RedirectToAction("Index", "Home"); // or Candidate if you prefer
    }

    // =========================
    // 📝 REGISTER
    // =========================

    // GET: /Auth/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Auth/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestDto model)
    {
        var client = _factory.CreateClient("ApiClient");

        var response = await client.PostAsJsonAsync("Auth/register", model);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Registration failed";
            return View(model);
        }

        return RedirectToAction("Login");
    }

    // =========================
    // 🚪 LOGOUT
    // =========================

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JWT");
        return RedirectToAction("Login");
    }
}