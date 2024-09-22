using Microsoft.AspNetCore.Mvc;
using UploadService.Models;
using UploadService.Services;

namespace UploadService.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountsController(AccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpPost("uploadAccount")]
    public async Task<IActionResult> UploadAccountFromApi([FromQuery] Account account)
    {
        await _accountService.UploadAccountFromApiDataAsync(account);
        return Ok();
    }

    [HttpPost("uploadAccountFile")]
    public async Task<IActionResult> UploadAccountFileFromApi([FromQuery] string accountFilePath)
    {
        await _accountService.UploadAccountFromFileAsync(accountFilePath);
        return Ok();
    }
    
}