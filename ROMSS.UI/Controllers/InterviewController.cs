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

    // List interviews for an application
    public async Task<IActionResult> Index(int applicationId, int jobId)
    {
        var client = GetClient();
        var response = await client.GetAsync($"Interview/application/{applicationId}");
        var data = response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<InterviewDto>>()
            : new List<InterviewDto>();

        ViewBag.ApplicationId = applicationId;
        ViewBag.JobId = jobId;
        return View(data);
    }

    // Schedule interview (GET)
    public IActionResult Schedule(int applicationId, int jobId)
    {
        ViewBag.ApplicationId = applicationId;
        ViewBag.JobId = jobId;
        return View();
    }

    // Schedule interview (POST)
    [HttpPost]
    public async Task<IActionResult> Schedule(ScheduleInterviewRequestDto model, int jobId)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Interview/schedule", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to schedule interview.";
            ViewBag.ApplicationId = model.JobApplicationId;
            ViewBag.JobId = jobId;
            return View(model);
        }
        return RedirectToAction("Index", new { applicationId = model.JobApplicationId, jobId });
    }

    // View feedbacks for an application
    public async Task<IActionResult> Feedback(int applicationId, int jobId)
    {
        var client = GetClient();
        var response = await client.GetAsync($"Interview/feedback/{applicationId}");
        var data = response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<InterviewFeedbackDto>>()
            : new List<InterviewFeedbackDto>();

        ViewBag.ApplicationId = applicationId;
        ViewBag.JobId = jobId;
        return View(data);
    }

    // Submit feedback (GET)
    public IActionResult SubmitFeedback(int applicationId, int interviewId, int jobId)
    {
        ViewBag.ApplicationId = applicationId;
        ViewBag.InterviewId = interviewId;
        ViewBag.JobId = jobId;
        return View();
    }

    // Submit feedback (POST)
    [HttpPost]
    public async Task<IActionResult> SubmitFeedback(SubmitFeedbackRequestDto model, int jobId)
    {
        var client = GetClient();
        var response = await client.PostAsJsonAsync("Interview/feedback", model);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to submit feedback.";
            ViewBag.ApplicationId = model.ApplicationId;
            ViewBag.InterviewId = model.InterviewId;
            ViewBag.JobId = jobId;
            return View(model);
        }

        // If Selected → go to Offer, else back to applications
        if (model.Result == "Selected")
            return RedirectToAction("Generate", "Offer",
                new { applicationId = model.ApplicationId, jobId });

        return RedirectToAction("Index", "JobApplication", new { jobId });
    }
}