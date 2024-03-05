using Domain.Models;
using Infrastructure.Services;


var transactionServ = new TransactionService();

var transaction = new Transaction()
{
    Sender_Id = 2,
    Recipient_Id = 1,
    Sum = 5000,
    DateOfTransaction = DateTime.Now
};
System.Console.WriteLine(transactionServ.Transaction(transaction));


var customerServ = new CustomerService();
var allTransactions = customerServ.GetCustomersTransactions();

foreach (var item1 in allTransactions)
{
    System.Console.Write("      Customer Id : ");
    System.Console.WriteLine(item1.Customer.Customer_Id);
    System.Console.Write("      Customer fullname : ");
    System.Console.WriteLine(item1.Customer.FullName);
    System.Console.Write("      Customer balance : ");
    System.Console.WriteLine(item1.Customer.Balance);
    foreach (var item2 in item1.Transactions)
    {
        System.Console.Write("Transaction Id : ");
        System.Console.WriteLine(item2.Transaction_Id);
        System.Console.Write("Sender Id : ");
        System.Console.WriteLine(item2.Sender_Id);
        System.Console.Write("Recipient Id : ");
        System.Console.WriteLine(item2.Recipient_Id);
        System.Console.Write("Transaction sum : ");
        System.Console.WriteLine(item2.Sum);
        System.Console.Write("Transaction date : ");
        System.Console.WriteLine(item2.DateOfTransaction);
        System.Console.WriteLine("-------------------------");
    }
    System.Console.WriteLine();
}


var transactions = transactionServ.GetTransactionsByDate(new DateTime(2024, 01, 01));
foreach (var item in transactions)
{
    System.Console.Write("Transaction Id : ");
    System.Console.WriteLine(item.Transaction_Id);
    System.Console.Write("Sender Id : ");
    System.Console.WriteLine(item.Sender_Id);
    System.Console.Write("Recipient Id : ");
    System.Console.WriteLine(item.Recipient_Id);
    System.Console.Write("Transaction sum : ");
    System.Console.WriteLine(item.Sum);
    System.Console.Write("Transaction date : ");
    System.Console.WriteLine(item.DateOfTransaction);
    System.Console.WriteLine("-------------------------");
}

var cashback = transactionServ.TransactionsCashback(new DateTime(2024, 02, 04));
System.Console.WriteLine(cashback);

