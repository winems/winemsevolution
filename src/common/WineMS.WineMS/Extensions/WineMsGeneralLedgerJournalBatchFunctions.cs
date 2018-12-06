using System.Linq;
using WineMS.WineMS.DataAccess;

namespace WineMS.WineMS.Extensions {

  public static class WineMsGeneralLedgerJournalBatchFunctions {

    private static IWineMsBufferEntry[] ToWineMsBufferEntryArray(this WineMsGeneralLedgerJournalTransaction[] transactions) =>
      transactions.Select(a => (IWineMsBufferEntry) a).ToArray();

    public static void CompletePosting(
      this WineMsGeneralLedgerJournalTransactionBatch generalLedgerJournalTransactionBatch,
      string integrationDocumentType)
    {
      WineMsDbContextFunctions
        .WrapInDbContext(
          context =>
          {
            var transactionLines = generalLedgerJournalTransactionBatch.Transactions.ToWineMsBufferEntryArray();

            context.SetAsPosted(transactionLines);
            context.AddIntegrationMappings(
              new IntegrationMappingDescriptor {
                IntegrationDocumentNumber = generalLedgerJournalTransactionBatch.DocumentNumber,
                IntegrationDocumentType = integrationDocumentType,
                TransactionLines = transactionLines
              });
            context.SaveChanges();
          });
    }

  }

}