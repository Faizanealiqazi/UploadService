using Microsoft.AspNetCore.Mvc;
using UploadService.Models;
using UploadService.Services;
namespace UploadService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(AccountService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("uploadAccount")]
        public async Task<IActionResult> UploadAccountFromApi([FromQuery] Account account)
        {
            _logger.LogInformation("UploadAccountFromApi() :: entered");
            
            if (account == null)
            {
                _logger.LogError("UploadAccountFileFromApi() :: Account data is null when uploading from API.");
                return BadRequest("Account data is required.");
            }

            try
            {
                await _accountService.UploadAccountFromApiDataAsync(account);
                _logger.LogInformation("UploadAccountFileFromApi() :: Successfully uploaded account from API: {@Account}", account);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadAccountFileFromApi() :: Error uploading account from API: {@Account}", account);
                return StatusCode(500, "An error occurred while uploading the account.");
            }
            finally
            {
                _logger.LogInformation("UploadAccountFromApi() :: exited");
            }
        }

        [HttpPost("uploadAccountFile")]
        public async Task<IActionResult> UploadAccountFileFromApi([FromQuery] string accountFilePath)
        {
            _logger.LogInformation("UploadAccountFileFromApi() :: entered");
            
            if (string.IsNullOrEmpty(accountFilePath))
            {
                _logger.LogWarning("UploadAccountFileFromApi() :: UploadAccountFileFromApi() :: File path is required.");
                return BadRequest("File path is required.");
            }

            if (!System.IO.File.Exists(accountFilePath))
            {
                _logger.LogWarning("UploadAccountFileFromApi() :: File not found: {FilePath}", accountFilePath);
                return NotFound("File not found.");
            }

            try
            {
                await _accountService.UploadAccountFromFileAsync(accountFilePath);
                _logger.LogInformation("UploadAccountFileFromApi() :: Successfully uploaded accounts from file: {FilePath}", accountFilePath);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadAccountFileFromApi() :: Error uploading accounts from file: {FilePath}", accountFilePath);
                return StatusCode(500, "An error occurred while uploading accounts from the file.");
            }
            finally
            {
                _logger.LogInformation("UploadAccountFileFromApi() :: exited");
            }
        }
    }
}
