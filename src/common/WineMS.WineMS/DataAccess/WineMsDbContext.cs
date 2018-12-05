using System;
using System.Data.Entity;
using System.Linq;
using WineMS.Common.DataAccess;

namespace WineMS.WineMS.DataAccess {

  public class WineMsDbContext : DbContext {

    public DbSet<IntegrationMapping> IntegrationMappings { get; set; }
    public DbSet<WineMsBufferEntry> WineMsBufferEntries { get; set; }
    public DbSet<WineMsPurchaseOrderTransaction> WineMsPurchaseOrderTransactions { get; set; }
    public DbSet<WineMsSalesOrderTransaction> WineMsSalesOrderTransactions { get; set; }
    public DbSet<WineMsJournalTransaction> WineMsJournalTransactions { get; set; }

    public WineMsDbContext() : base(DatabaseConstants.WineMsConnectionStringName) { }

    public WineMsJournalTransactionBatch[] ListNewWineMsJournalTransactions() =>
      WineMsJournalTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new {a.CompanyId, a.TransactionType, a.DocumentNumber})
        .Select(
          a => new WineMsJournalTransactionBatch {
            CompanyId = a.Key.CompanyId,
            DocumentNumber = a.Key.DocumentNumber,
            TransactionType = a.Key.TransactionType,
            Transactions = a.ToArray()
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public WineMsOrderTransactionDocument[] ListNewWineMsSalesOrderTransactions() =>
      WineMsSalesOrderTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new {a.CompanyId, a.TransactionType, a.DocumentNumber})
        .Select(
          a => (WineMsOrderTransactionDocument) new WineMsSalesOrderTransactionDocument {
            CustomerAccountCode = a.FirstOrDefault()?.CustomerAccountCode ?? "",
            CompanyId = a.Key.CompanyId,
            DocumentDiscountPercentage = a.FirstOrDefault()?.DocumentDiscountPercentage ?? 0,
            DocumentNumber = a.Key.DocumentNumber,
            TransactionDate = a.FirstOrDefault()?.TransactionDate ?? DateTime.MinValue,
            TransactionType = a.Key.TransactionType,
            TransactionLines = a.Cast<IWineMsTransactionLine>().ToArray()
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public WineMsOrderTransactionDocument[] ListNewWineMsPurchaseOrderTransactions() =>
      WineMsPurchaseOrderTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new {a.CompanyId, a.TransactionType, a.DocumentNumber})
        .Select(
          a => (WineMsOrderTransactionDocument) new WineMsPurchaseOrderTransactionDocument {
            SupplierAccountCode = a.FirstOrDefault()?.SupplierAccountCode ?? "",
            CompanyId = a.Key.CompanyId,
            DocumentNumber = a.Key.DocumentNumber,
            TransactionDate = a.FirstOrDefault()?.TransactionDate ?? DateTime.MinValue,
            TransactionType = a.Key.TransactionType,
            TransactionLines = a.Cast<IWineMsTransactionLine>().ToArray()
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public void SetAsPosted(IWineMsBufferEntry[] wineMsBufferEntries)
    {
      foreach (var transactionLine in wineMsBufferEntries)
        SetAsPosted(transactionLine);
    }

    private void SetAsPosted(IWineMsBufferEntry wineMsBufferEntry)
    {
      var wineMsTransaction =
        WineMsBufferEntries
          .FirstOrDefault(a => a.Guid == wineMsBufferEntry.Guid);
      if (wineMsTransaction != null)
        wineMsTransaction.PostedToAccountingSystem = 1;
    }

    public void AddIntegrationMappings(IntegrationMappingDescriptor integrationMappingDescriptor)
    {
      foreach (var transactionLine in integrationMappingDescriptor.TransactionLines) {
        transactionLine.PostedToAccountingSystem = 1;
        IntegrationMappings.Add(
          new IntegrationMapping {
            CompanyId = transactionLine.CompanyId,
            Guid = transactionLine.Guid,
            IntegrationDocumentNumber = integrationMappingDescriptor.IntegrationDocumentNumber,
            IntegrationDocumentType = integrationMappingDescriptor.IntegrationDocumentType
          });
      }
    }

  }

}