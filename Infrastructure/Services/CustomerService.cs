using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class CustomerService
{
    private readonly DapperContext _context;

    public CustomerService()
    {
        _context = new DapperContext();
    }

    public void AddCustomer(Customer customer)
    {
        var sql = @"insert into Customers(FullName,Balance) values(@FullName,@Balance);";
        _context.Connection().Execute(sql, customer);
    }

    public List<Customer> GetCustomers()
    {
        var sql = @"select * from Customers;";
        return _context.Connection().Query<Customer>(sql).ToList();
    }

    public void UpdateCustomer(Customer customer)
    {
        var sql = @"update Customers set FullName=@FullName,Balance=@Balance  where Customer_Id = @Customer_Id ";
        _context.Connection().Execute(sql, customer);
    }

    public void DeleteCustomer(int id)
    {
        var sql = @"delete from Customer where Customer_Id = @Customer_Id;";
        _context.Connection().Execute(sql, new { Id = id });
    }


    // 3
    public List<CustomerTransactions> GetCustomersTransactions()
    {
        var sql = @"select Customer_Id from Customers;";
        var customers_Id = _context.Connection().Query<int>(sql).ToList();

        var sql1 = @"select * from Customers where Customer_Id=@Customer_Id;
        select * from Transactions where Sender_Id=@Customer_Id or Recipient_Id=@Customer_Id;";

        var customersTransactions = new List<CustomerTransactions>();

        foreach (var item in customers_Id)
        {
            using (var multiple = _context.Connection().QueryMultiple(sql1, new { Customer_Id = item }))
            {
                var cusTransactions = new CustomerTransactions();
                cusTransactions.Customer = multiple.ReadFirst<Customer>();
                cusTransactions.Transactions = multiple.Read<Transaction>().ToList();
                customersTransactions.Add(cusTransactions);
            }
        }
        return customersTransactions;

    }












}
