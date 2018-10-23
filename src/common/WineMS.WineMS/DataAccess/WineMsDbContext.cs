using System.Data.Entity;
using System.Linq;
using WineMS.Common.DataAccess;

namespace WineMS.WineMS.DataAccess {

  public class WineMsDbContext : DbContext {

    public DbSet<WineMsTransaction> WineMsTransactions { get; set; }

    public WineMsDbContext() : base(DatabaseConstants.WineMsConnectionStringName) { }

    public WineMsTransactionDocument[] ListNewWineMsTransactions(string[] transactionTypes) =>
      WineMsTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0 && transactionTypes.Contains(a.TransactionType))
        .GroupBy(a => new {a.CompanyId, a.TransactionType, a.DocumentNumber})
        .Select(
          a => new WineMsTransactionDocument {
            CompanyId = a.Key.CompanyId,
            DocumentNumber = a.Key.DocumentNumber,
            TransactionType = a.Key.TransactionType,
            TransactionLines = a.ToArray()
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

  }

  public struct WineMsTransactionDocument {

    public int BranchId { get; set; }
    public string CompanyId { get; set; }
    public string DocumentNumber { get; set; }
    public WineMsTransaction[] TransactionLines { get; set; }
    public string TransactionType { get; set; }

  }

}