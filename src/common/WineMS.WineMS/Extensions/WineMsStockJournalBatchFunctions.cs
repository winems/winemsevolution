using System.Linq;
using WineMS.WineMS.DataAccess;

namespace WineMS.WineMS.Extensions {

  public static class WineMsStockJournalBatchFunctions {

    public static void CompletePosting(
      this WineMsStockJournalTransactionBatch transactionBatch,
      string integrationDocumentType) {
      WineMsDbContextFunctions
        .WrapInDbContext(
          context => {
            var transactionLines = transactionBatch.Transactions.ToWineMsBufferEntryArray();

            context.SetAsPosted(transactionLines);

            context
              .IntegrationMappings
              .AddRange(
                transactionBatch
                  .Transactions
                  .Select(
                    transactionLine => new IntegrationMapping {
                      CompanyId = transactionLine.CompanyId,
                      Guid = transactionLine.Guid,
                      IntegrationDocumentNumber = transactionLine.ReferenceNumber,
                      IntegrationDocumentType = integrationDocumentType
                    })
                  .ToArray());

            context.SaveChanges();
          });
    }

    private static IWineMsBufferEntry[] ToWineMsBufferEntryArray(this WineMsStockJournalTransaction[] transactions) =>
      transactions.Select(a => (IWineMsBufferEntry) a).ToArray();

  }

}