using System;
using System.Data.Entity;
using System.Linq;
using WineMS.Common.DataAccess;

namespace WineMS.WineMS.DataAccess {

  public class WineMsDbContext: DbContext {

    public DbSet<IntegrationMapping> IntegrationMappings { get; set; }

    public DbSet<WineMsBufferEntry> WineMsBufferEntries { get; set; }

    public DbSet<WineMsCreditNoteTransaction> WineMsCreditNoteTransactions { get; set; }

    public DbSet<WineMsGeneralLedgerJournalTransaction> WineMsJournalTransactions { get; set; }

    public DbSet<WineMsPurchaseOrderTransaction> WineMsPurchaseOrderTransactions { get; set; }

    public DbSet<WineMsReturnToSupplierTransaction> WineMsReturnToSupplierTransactions { get; set; }

    public DbSet<WineMsSalesOrderTransaction> WineMsSalesOrderTransactions { get; set; }

    public DbSet<WineMsStockJournalTransaction> WineMsStockJournalTransactions { get; set; }

    public WineMsDbContext() : base(DatabaseConstants.WineMsConnectionStringName) {
    }

    public WineMsTransaction[] ListNewWineMsGeneralLedgerJournalTransactions() =>
      WineMsJournalTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new { a.CompanyId, a.TransactionType, a.DocumentNumber })
        .Select(
          a => new WineMsGeneralLedgerJournalTransactionBatch {
            CompanyId = a.Key.CompanyId,
            DocumentNumber = a.Key.DocumentNumber,
            TransactionType = a.Key.TransactionType,
            Transactions = a.ToArray()
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .Cast<WineMsTransaction>()
        .ToArray();

    public WineMsOrderTransactionDocument[] ListNewWineMsSalesOrderTransactions() =>
      WineMsSalesOrderTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new { a.CompanyId, a.TransactionType, a.DocumentNumber })
        .Select(
          a => {
            var firstTransaction = a.FirstOrDefault();

            return (WineMsOrderTransactionDocument)new WineMsSalesOrderTransactionDocument {
              CustomerAccountCode = firstTransaction?.CustomerAccountCode ?? "",
              CompanyId = a.Key.CompanyId,
              DocumentDiscountPercentage = firstTransaction?.DocumentDiscountPercentage ?? 0,
              DocumentNumber = a.Key.DocumentNumber,
              ExchangeRate = firstTransaction?.ExchangeRate ?? 0,
              MessageLine1 = firstTransaction?.MessageLine1 ?? "",
              MessageLine2 = firstTransaction?.MessageLine2 ?? "",
              MessageLine3 = firstTransaction?.MessageLine3 ?? "",
              TransactionDate = firstTransaction?.TransactionDate ?? DateTime.MinValue,
              TransactionType = a.Key.TransactionType,
              TransactionLines = a.Cast<IWineMsTransactionLine>().ToArray()
            };
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public WineMsTransaction[] ListNewWineMsStockJournalTransactions() =>
      WineMsStockJournalTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new { a.CompanyId })
        .Select(
          a => new WineMsStockJournalTransactionBatch {
            CompanyId = a.Key.CompanyId,
            Transactions = a.ToArray()
          })
        .OrderBy(a => a.CompanyId)
        .Cast<WineMsTransaction>()
        .ToArray();

    public WineMsOrderTransactionDocument[] ListNewWineMsCreditNoteTransactions() =>
      WineMsCreditNoteTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new { a.CompanyId, a.TransactionType, a.DocumentNumber })
        .Select(
          a => {
            var firstTransaction = a.FirstOrDefault();

            return (WineMsOrderTransactionDocument)new WineMsCreditNoteTransactionDocument {
              CustomerAccountCode = firstTransaction?.CustomerAccountCode ?? "",
              CompanyId = a.Key.CompanyId,
              DocumentDiscountPercentage = firstTransaction?.DocumentDiscountPercentage ?? 0,
              DocumentNumber = a.Key.DocumentNumber,
              ExchangeRate = firstTransaction?.ExchangeRate ?? 0,
              MessageLine1 = firstTransaction?.MessageLine1 ?? "",
              MessageLine2 = firstTransaction?.MessageLine2 ?? "",
              MessageLine3 = firstTransaction?.MessageLine3 ?? "",
              TransactionDate = firstTransaction?.TransactionDate ?? DateTime.MinValue,
              TransactionType = a.Key.TransactionType,
              TransactionLines = a.Cast<IWineMsTransactionLine>().ToArray()
            };
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public WineMsOrderTransactionDocument[] ListNewWineMsPurchaseOrderTransactions() => WineMsPurchaseOrderTransactions
        .Where(a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new { a.CompanyId, a.TransactionType, a.DocumentNumber })
        .Select(a => {
          var transaction = a.FirstOrDefault();
          return (WineMsOrderTransactionDocument)new WineMsPurchaseOrderTransactionDocument {
            CompanyId = a.Key.CompanyId,
            DocumentNumber = a.Key.DocumentNumber,
            ExchangeRate = transaction?.ExchangeRate ?? 1,
            SupplierAccountCode = transaction?.SupplierAccountCode ?? "",
            SupplierInvoiceNumber = a.FirstOrDefault()?.SupplierInvoiceNumber ?? "",
            TransactionDate = a.FirstOrDefault()?.TransactionDate ?? DateTime.MinValue,
            TransactionType = a.Key.TransactionType,
            TransactionLines = a.Cast<IWineMsTransactionLine>().ToArray()
          };
        })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public WineMsOrderTransactionDocument[] ListNewWineMsReturnToSupplierTransactions() =>
      WineMsReturnToSupplierTransactions
        .Where(
          a => a.PostedToAccountingSystem == 0)
        .ToArray()
        .GroupBy(a => new { a.CompanyId, a.TransactionType, a.DocumentNumber })
        .Select(
          a => {
            var transaction = a.FirstOrDefault();
            return (WineMsOrderTransactionDocument)new WineMsReturnToSupplierTransactionDocument {
              CompanyId = a.Key.CompanyId,
              DocumentNumber = a.Key.DocumentNumber,
              ExchangeRate = transaction?.ExchangeRate ?? 1,
              SupplierAccountCode = transaction?.SupplierAccountCode ?? "",
              SupplierInvoiceNumber = a.FirstOrDefault()?.SupplierInvoiceNumber ?? "",
              TransactionDate = a.FirstOrDefault()?.TransactionDate ?? DateTime.MinValue,
              TransactionType = a.Key.TransactionType,
              TransactionLines = a.Cast<IWineMsTransactionLine>().ToArray()
            };
          })
        .OrderBy(a => a.CompanyId)
        .ThenBy(a => a.TransactionType)
        .ThenBy(a => a.DocumentNumber)
        .ToArray();

    public void SetAsPosted(IWineMsBufferEntry[] wineMsBufferEntries) {
      foreach (var transactionLine in wineMsBufferEntries)
        SetAsPosted(transactionLine);
    }

    private void SetAsPosted(IWineMsBufferEntry wineMsBufferEntry) {
      var wineMsTransaction =
        WineMsBufferEntries
          .FirstOrDefault(a => a.Guid == wineMsBufferEntry.Guid);
      if (wineMsTransaction != null)
        wineMsTransaction.PostedToAccountingSystem = 1;
    }

    public void AddIntegrationMappings(IntegrationMappingDescriptor integrationMappingDescriptor) {
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