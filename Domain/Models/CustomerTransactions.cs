namespace Domain.Models;

public class CustomerTransactions
{
    public Customer Customer { get; set; }
    public List<Transaction> Transactions { get; set; }
}
