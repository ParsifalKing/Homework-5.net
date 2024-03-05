namespace Domain.Models;

public class Transaction
{
    public int Transaction_Id { get; set; }
    public int Sender_Id { get; set; }
    public int Recipient_Id { get; set; }
    public double Sum { get; set; }
    public DateTime DateOfTransaction { get; set; }
}
