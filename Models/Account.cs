namespace UploadService.Models;

public class Account
{
    public int id { get; set; }
    public string userId { get; set; }
    public string accountName { get; set; }
    public string balance { get; set; }
    public string currency { get; set; }
    public string status { get; set; }
    public DateTime createAt { get; set; }
    public DateTime updateAt { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
}