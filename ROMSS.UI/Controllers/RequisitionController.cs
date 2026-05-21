using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

public class RequisitionController : Controller
{
    private readonly IHttpClientFactory _factory;

    public RequisitionController(IHttpClientFactory factory)
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

    public async Task<IActionResult> Index()
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<List<RequisitionDto>>("Requisition");
        return View(data);
    }

    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(AddRequisitionRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Requisition", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to add requisition.";
            return View(model);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<UpdateRequisitionRequestDto>($"Requisition/{id}");
        if (data == null) return NotFound();
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateRequisitionRequestDto model)
    {
        var client = GetClient();
        var response = await client.PutAsJsonAsync($"Requisition/{model.Id}", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to update requisition.";
            return View(model);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var client = GetClient();
        await client.DeleteAsync($"Requisition/{id}");
        return RedirectToAction("Index");
    }
}