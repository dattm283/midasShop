using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Entites;

public class Transaction
{
    public int Id { set; get; }
    public DateTime TransactionDate { set; get; }
    public string ExternalTransactionId { set; get; }
    public decimal Amount { set; get; }
    public decimal Fee { set; get; }
    public string Result { set; get; }
    public string Message { set; get; }
    public TransactionStatus Status { set; get; }
    public string Provider { set; get; }

    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    
}