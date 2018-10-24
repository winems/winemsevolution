using System;
using System.Data.Entity;
using System.Linq;
using WineMS.Common.DataAccess;

namespace WineMS.WineMS.DataAccess {

  public class WineMsDbContext : DbContext {

    public DbSet<IntegrationMapping> IntegrationMappings { get; set; }
    public DbSet<WineMsTransaction> WineMsTransactions { get; set; }

    public WineMsDbContext() : base(DatabaseConstants.WineMsConnectionStringName) { }

    public WineMsTransactionDocument[] ListNewWineMsTransactions(string[] transactionTypes) =>
      WineMsTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0 && transactionTypes.Contains(a.TransactionType))
        .ToArray()
        .GroupBy(a => new {a.CompanyId, a.TransactionType, a.DocumentNumber})
        .Select(
          a => new WineMsTransactionDocument {
            CustomerSupplierAccountCode = a.FirstOrDefault()?.CustomerSupplierAccountCode ?? "",
            CompanyId = a.Key.CompanyId,
            DocumentNumber = a.Key.DocumentNumber,
            TransactionDate = a.FirstOrDefault()?.TransactionDate ?? DateTime.MinValue,
            TransactionType = a.Key.TransactionType,
            TransactionLines = a.ToArray()
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public void SetAsPosted(WineMsTransactionDocument transactionDocument)
    {
      foreach (var transactionLine in transactionDocument.TransactionLines) {
        var wineMsTransaction =
          WineMsTransactions.FirstOrDefault(a => a.Guid == transactionLine.Guid);
        if (wineMsTransaction == null) continue;
        wineMsTransaction.PostedToAccountingSystem = 1;
      }
    }

    public void AddIntegrationMappings(
      WineMsTransactionDocument transactionDocument,
      string integrationDocumentType)
    {
      foreach (var transactionLine in transactionDocument.TransactionLines) {
        transactionLine.PostedToAccountingSystem = 1;
        IntegrationMappings.Add(
          new IntegrationMapping {
            CompanyId = transactionLine.CompanyId,
            Guid = transactionLine.Guid,
            IntegrationDocumentNumber = transactionDocument.IntegrationDocumentNumber,
            IntegrationDocumentType = integrationDocumentType
          });
      }
    }

  }

}