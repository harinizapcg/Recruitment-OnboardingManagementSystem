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

    public async Task<IActionResult> Apply(int jobId)
    {
        var client = GetClient();
        var candidates = await client.GetFromJsonAsync<List<CandidateDto>>("Candidate");
        ViewBag.JobId = jobId;
        ViewBag.Candidates = candidates ?? new List<CandidateDto>();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Apply(int jobId, int candidateId,
        IFormFile resume, IFormFile? coverLetter)
    {
        var client = GetClient();

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(jobId.ToString()), "JobId");
        content.Add(new StringContent(candidateId.ToString()), "CandidateId");

        if (resume != null && resume.Length > 0)
        {
            var resumeStream = resume.OpenReadStream();
            var resumeContent = new StreamContent(resumeStream);
            resumeContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(resume.ContentType);
            content.Add(resumeContent, "Resume", resume.FileName);
        }
        else
        {
            var candidates = await client.GetFromJsonAsync<List<CandidateDto>>("Candidate");
            ViewBag.Error = "Resume is required.";
            ViewBag.JobId = jobId;
            ViewBag.Candidates = candidates ?? new List<CandidateDto>();
            return View();
        }

        if (coverLetter != null && coverLetter.Length > 0)
        {
            var clStream = coverLetter.OpenReadStream();
            var clContent = new StreamContent(clStream);
            clContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(coverLetter.ContentType);
            content.Add(clContent, "CoverLetter", coverLetter.FileName);
        }

        var response = await client.PostAsync("JobApplication/apply", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            var candidates = await client.GetFromJsonAsync<List<CandidateDto>>("Candidate");
            ViewBag.Error = $"Failed to apply: {error}";
            ViewBag.JobId = jobId;
            ViewBag.Candidates = candidates ?? new List<CandidateDto>();
            return View();
        }

        return RedirectToAction("Index", new { jobId });
    }

    [HttpPost]
    public async Task<IActionResult> Shortlist(int id, [FromForm] int jobId)
    {
        var client = GetClient();
        var response = await client.PutAsync($"JobApplication/shortlist/{id}", null);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"Shortlist failed: {response.StatusCode} - {error}";
        }
        return RedirectToAction("Index", new { jobId });
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id, [FromForm] int jobId)
    {
        var client = GetClient();
        var response = await client.PutAsync($"JobApplication/reject/{id}", null);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"Reject failed: {response.StatusCode} - {error}";
        }
        return RedirectToAction("Index", new { jobId });
    }
}