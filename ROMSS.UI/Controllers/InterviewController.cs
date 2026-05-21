using Microsoft.AspNetCore.Mvc;
using ROMSS.UI.Models.DTO;
using System.Net.Http.Headers;

public class InterviewController : Controller
{
    private readonly IHttpClientFactory _factory;

    public InterviewController(IHttpClientFactory factory)
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
        var data = await client.GetFromJsonAsync<List<InterviewDto>>($"Interview/application/{applicationId}");
        ViewBag.ApplicationId = applicationId;
        return View(data);
    }

    public IActionResult Schedule(int applicationId)
    {
        ViewBag.ApplicationId = applicationId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Schedule(ScheduleInterviewRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Interview/schedule", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to schedule interview.";
            return View(model);
        }
        return RedirectToAction("Index", new { applicationId = model.JobApplicationId });
    }

    public async Task<IActionResult> Feedback(int applicationId)
    {
        var client = GetClient();
        var data = await client.GetFromJsonAsync<List<InterviewFeedbackDto>>($"Interview/feedback/{applicationId}");
        ViewBag.ApplicationId = applicationId;
        return View(data);
    }

    public IActionResult SubmitFeedback(int applicationId, int interviewId)
    {
        ViewBag.ApplicationId = applicationId;
        ViewBag.InterviewId = interviewId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitFeedback(SubmitFeedbackRequestDto model)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Interview/feedback", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to submit feedback.";
            return View(model);
        }
        return RedirectToAction("Feedback", new { applicationId = model.ApplicationId });
    }
}