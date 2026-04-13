using MediatR;

public class UploadDocumentsCommand : IRequest<int>
{
    public int ApplicationId { get; set; }    
    public string DocumentPath { get; set; }
}