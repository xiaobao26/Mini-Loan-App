using Microsoft.AspNetCore.Mvc;
using Mini_Loan_App.Controllers.Dtos;
using Mini_Loan_App.Services.Interfaces;

namespace Mini_Loan_App.Controllers;

[Route("[controller]")]
[ApiController]
public class LoansController: ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost]
    public async Task<ActionResult<LoanResponse>> CreateLoan([FromBody] CreateLoanRequest request)
    {
        var loan = await _loanService.CreateAsync(request);

        var response = new LoanResponse(
            loan.Id,
            loan.ApplicantName,
            loan.Principal,
            loan.AnnualRate,
            loan.TermMonths,
            loan.CreatedAt,
            loan.Status
        );
        
        return Ok( response );
    }


    [HttpGet("id")]
    public async Task<ActionResult<LoanResponse>> GetLoanById(Guid id)
    {
        var loan = await _loanService.GetByIdAsync(id);
        if (loan is null)
        {
            return NotFound();
        }

        var response = new LoanResponse(
            loan.Id,
            loan.ApplicantName,
            loan.Principal,
            loan.AnnualRate,
            loan.TermMonths,
            loan.CreatedAt,
            loan.Status
        );
        return Ok(response);
    }
}