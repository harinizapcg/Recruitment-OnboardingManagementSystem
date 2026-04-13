using MediatR;

public class GenerateOfferCommand : IRequest<int>
{
    public int ApplicationId { get; set; }
    public decimal Salary { get; set; }
    public DateTime JoiningDate { get; set; }
}