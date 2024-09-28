namespace UploadService.Models;

public class Account
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string AccountName { get; set; }
    public string Balance { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}