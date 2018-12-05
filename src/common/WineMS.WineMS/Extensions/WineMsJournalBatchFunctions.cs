using System;
using System.Linq;
using CSharpFunctionalExtensions;
using WineMS.WineMS.DataAccess;

namespace WineMS.WineMS.Extensions {

  public static class WineMsJournalBatchFunctions {

    public static Result ForEachJournalBatchTransaction(
      Func<IWineMsBufferEntry, Result> func,
      IWineMsBufferEntry[] wineMsBufferEntries)
    {
      var result = Result.Ok();
      foreach (var transactionLine in wineMsBufferEntries)
      {
        result = func(transactionLine);
        if (result.IsFailure) return result;
      }

      return result;
    }

    public static IWineMsBufferEntry[] ToWineMsBufferEntryArray(this WineMsJournalTransaction[] transactions) =>
      transactions.Select(a => (IWineMsBufferEntry) a).ToArray();


    public static void CompletePosting(
      this WineMsJournalTransactionBatch journalTransactionBatch,
      string integrationDocumentType)
    {
      WineMsDbContextFunctions
        .WrapInDbContext(
          context =>
          {
            var transactionLines = journalTransactionBatch.Transactions.ToWineMsBufferEntryArray();

            context.SetAsPosted(transactionLines);
            context.AddIntegrationMappings(
              new IntegrationMappingDescriptor
              {
                IntegrationDocumentNumber = journalTransactionBatch.IntegrationDocumentNumber,
                IntegrationDocumentType = integrationDocumentType,
                TransactionLines = transactionLines
              });
            context.SaveChanges();
          });
    }


  }

}