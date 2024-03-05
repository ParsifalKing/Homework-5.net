using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class TransactionService
{

    readonly DapperContext _context;

    public TransactionService()
    {
        _context = new DapperContext();
    }

    // 2
    public string Transaction(Transaction transaction)
    {
        var sql1 = @"select * from Customers where Customer_Id = @Customer_Id";
        var senderCustomer = _context.Connection().QueryFirstOrDefault<Customer>(sql1, new { Customer_Id = transaction.Sender_Id });
        var sql2 = @"select * from Customers where Customer_Id = @Customer_Id";
        var recipientCustomer = _context.Connection().QueryFirstOrDefault<Customer>(sql2, new { Customer_Id = transaction.Recipient_Id });

        var sql = @"insert into Transactions(Sender_Id,Recipient_Id,Sum,DateOfTransaction)
        values(@Sender_Id,@Recipient_Id,@Sum,@DateOfTransaction) ";
        _context.Connection().Execute(sql, transaction);

        if (senderCustomer == null)
        {
            return "Sender customer not found";
        }
        if (recipientCustomer == null)
        {
            return "Recipient customer not found";
        }
        if (senderCustomer.Balance < transaction.Sum)
        {
            return "Sender customer have not balance for this transaction";
        }

        var senderCustomerBalance = senderCustomer.Balance - transaction.Sum;
        var sql3 = @"update Customers set Balance = @Balance where Customer_Id=@Customer_Id ";
        _context.Connection().Execute(sql3, new { Balance = senderCustomerBalance, Customer_Id = senderCustomer.Customer_Id });

        var recipientCustomerBalance = recipientCustomer.Balance + transaction.Sum;
        var sql4 = @"update Customers set Balance = @Balance where Customer_Id=@Customer_Id ";
        _context.Connection().Execute(sql4, new { Balance = recipientCustomerBalance, Customer_Id = recipientCustomer.Customer_Id });

        return "Transaction finished succusfully!!!";
    }

    // 4
    public List<Transaction> GetTransactionsByDate(DateTime transactionDate)
    {
        var sql = @"select * from Transactions where DateOfTransaction >= @DateOfTransaction;";
        var result = _context.Connection().Query<Transaction>(sql, new { DateOfTransaction = transactionDate }).ToList();
        return result;
    }


    // Supplementary method -  the aim of this method is cashback money to that customers who sent more than 5000 somoni in definite time
    public string TransactionsCashback(DateTime transactionDate)
    {
        var sql = @"select * from Transactions where Sum >= 5000 and DateOfTransaction >= @DateOfTransaction;";
        var transactionCashback = _context.Connection().Query<Transaction>(sql, new { DateOfTransaction = transactionDate }).ToList();
        if (transactionCashback == null)
        {
            return $"Nobody sent more than 5000 somoni in this time";
        }
        foreach (var item in transactionCashback)
        {
            item.Sum = item.Sum / 100 * 0.3;
            var sql1 = @"select * from Customers where Customer_Id = @Customer_Id";
            var customerCashback = _context.Connection().QueryFirst<Customer>(sql1, new { Customer_Id = item.Sender_Id });
            customerCashback.Balance += item.Sum;

            var sql2 = @"update Customers set Balance = @Balance where Customer_Id=@Customer_Id";
            _context.Connection().Execute(sql2, new { Balance = customerCashback.Balance, Customer_Id = customerCashback.Customer_Id });
        }
        return $"Cashback sent succefully!!!";

    }


}

