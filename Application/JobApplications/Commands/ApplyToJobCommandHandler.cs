using Application.JobApplications.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

public class ApplyToJobHandler : IRequestHandler<ApplyToJobCommand, int>
{
    private readonly IJobApplicationRepository _repo;

    public ApplyToJobHandler(IJobApplicationRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(ApplyToJobCommand request, CancellationToken cancellationToken)
    {
        // ✅ Validate resume (important)
        if (request.Resume == null)
            throw new Exception("Resume is required");

        // ✅ Save files
        var resumePath = await SaveFile(request.Resume, "resumes");

        var coverLetterPath = request.CoverLetter != null
            ? await SaveFile(request.CoverLetter, "coverletters")
            : null;

        // ✅ Create entity
        var application = new JobApplication
        {
            JobId = request.JobId,
            CandidateId = request.CandidateId,
            ResumePath = resumePath,
            CoverLetterPath = coverLetterPath,
            AppliedAt = DateTime.UtcNow,
            Status = "Pending"
        };

        // ✅ Save to DB
        var result = await _repo.CreateAsync(application);

        return result.Id; // return ID instead of full object
    }
    private async Task<string> SaveFile(IFormFile file, string folder)
    {
        // ✅ Store inside wwwroot so browser can access
        var dir = Path.Combine("wwwroot", "uploads", folder);

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(dir, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        // ✅ Return relative path (used in DB + browser)
        return Path.Combine("uploads", folder, fileName).Replace("\\", "/");
    }
    // ✅ Helper method
    //private async Task<string> SaveFile(IFormFile file, string folder)
    //{
    //    var dir = Path.Combine("uploads", folder);

    //    if (!Directory.Exists(dir))
    //        Directory.CreateDirectory(dir);

    //    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    //    var path = Path.Combine(dir, fileName);

    //    using var stream = new FileStream(path, FileMode.Create);
    //    await file.CopyToAsync(stream);

    //    return path;
    //}
}