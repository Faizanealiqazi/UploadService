using UploadService.Data;
using UploadService.Models;

namespace UploadService.Services
{
    public class AccountService
    {
        private const int _columnSize = 10;
        private readonly UploadServiceContext _context;
        private readonly ILogger<AccountService> _logger;

        public AccountService(UploadServiceContext context, ILogger<AccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task UploadAccountFromFileAsync(string filePath)
        {
            _logger.LogInformation("UploadAccountFromFileAsync() :: entered");
            
            var lines = await File.ReadAllLinesAsync(filePath);
            var accounts = new List<Account>();

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var data = line.Split(',');

                if (data.Length != _columnSize)
                {
                    _logger.LogWarning("UploadAccountFromFileAsync() :: Line {LineIndex} in file {FilePath} does not have the correct number of columns", i, filePath);
                    continue;
                }

                try
                {
                    var account = new Account
                    {
                        Id = int.Parse(data[0]),
                        UserId = data[1],
                        AccountName = data[2],
                        Balance = data[3],
                        Currency = data[4],
                        Status = data[5],
                        CreateAt = DateTime.Parse(data[6]),
                        UpdateAt = DateTime.Parse(data[7]),
                        Email = data[8],
                        Phone = data[9],
                    };
                    accounts.Add(account);
                }
                catch (FormatException ex)
                {
                    _logger.LogError(ex, "UploadAccountFromFileAsync() :: Error parsing line {LineIndex} in file {FilePath}: {Line}", i, filePath, line);
                }
            }

            await _context.Accounts.AddRangeAsync(accounts);
            await _context.SaveChangesAsync();
            _logger.LogInformation("UploadAccountFromFileAsync() :: Successfully uploaded {Count} accounts from file: {FilePath}", accounts.Count, filePath);
        }
        public async Task UploadAccountFromApiDataAsync(Account account)
        {
            _logger.LogInformation("UploadAccountFromApiDataAsync() :: entered");
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            _logger.LogInformation("UploadAccountFromApiDataAsync() :: Successfully uploaded account from API: {@Account}", account);
        }
    }
}