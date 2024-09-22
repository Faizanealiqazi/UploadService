using UploadService.Data;
using UploadService.Models;

namespace UploadService.Services;

public class AccountService
{
    private readonly UploadServiceContext _context;

    public AccountService(UploadServiceContext context)
    {
        _context = context;
    }

    public async Task UploadAccountFromFileAsync(string filePath)
    {
        var lines = await File.ReadAllLinesAsync(filePath);

        foreach (var line in lines)
        {
            var data = line.Split(',');
            var account = new Account
            {
                id = int.Parse(data[0]),
                userId = data[1],
                accountName = data[2],
                balance = data[3],
                currency = data[4],
                status = data[5],
                createAt = DateTime.Parse(data[6]),
                updateAt = DateTime.Parse(data[7]),
                email = data[8],
                phone = data[9],
            };
            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();

        }
    }
    public async Task UploadAccountFromApiDataAsync(Account account)
    {
        await _context.Account.AddAsync(account);
        await _context.SaveChangesAsync();
    }
}