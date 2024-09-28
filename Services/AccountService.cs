using UploadService.Data;
using UploadService.Models;

namespace UploadService.Services;

public class AccountService
{                        
    private const int _columnSize = 10;
    private readonly UploadServiceContext _context;

    public AccountService(UploadServiceContext context)
    {
        _context = context;
    }

    public async Task UploadAccountFromFileAsync(string filePath)
    {
        var lines = await File.ReadAllLinesAsync(filePath);

        var accounts = new List<Account>();

        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var data = line.Split(',');
        
            if (data.Length != _columnSize)
            {
                continue;
            }
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
        await _context.Accounts.AddRangeAsync(accounts);
        await _context.SaveChangesAsync();
    }
    public async Task UploadAccountFromApiDataAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }
}