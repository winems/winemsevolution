using System.Linq;
using WineMS.WineMS.DataAccess;

namespace WineMS.WineMS.Extensions {

  public static class WineMsGeneralLedgerJournalBatchFunctions {

    public static void CompletePosting(
      this WineMsGeneralLedgerJournalTransactionBatch transactionBatch,
      string integrationDocumentType) {
      WineMsDbContextFunctions
        .WrapInDbContext(
          context => {
            var transactionLines = transactionBatch.Transactions.ToWineMsBufferEntryArray();

            context.SetAsPosted(transactionLines);
            context.AddIntegrationMappings(
              new IntegrationMappingDescriptor {
                IntegrationDocumentNumber = transactionBatch.DocumentNumber,
                IntegrationDocumentType = integrationDocumentType,
                TransactionLines = transactionLines
              });
            context.SaveChanges();
          });
    }

    private static IWineMsBufferEntry[] ToWineMsBufferEntryArray(this WineMsGeneralLedgerJournalTransaction[] transactions) =>
      transactions.Select(a => (IWineMsBufferEntry) a).ToArray();

  }

}