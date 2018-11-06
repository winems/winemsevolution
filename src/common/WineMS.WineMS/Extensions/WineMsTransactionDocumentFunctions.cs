using System;
using CSharpFunctionalExtensions;
using WineMS.WineMS.DataAccess;

namespace WineMS.WineMS.Extensions {

  public static class WineMsTransactionDocumentFunctions {

    public static Result ForEachTransactionDocumentLine(
      Func<IWineMsTransactionLine, Result> func,
      IWineMsTransactionLine[] wineMsBufferEntries)
    {
      var result = Result.Ok();
      foreach (var transactionLine in wineMsBufferEntries) {
        result = func(transactionLine);
        if (result.IsFailure) return result;
      }

      return result;
    }

    public static void CompletePosting(
      this IWineMsTransactionDocument document,
      string integrationDocumentType)
    {
      WineMsDbContextFunctions
        .WrapInDbContext(
          context =>
          {
            context.SetAsPosted(document.TransactionLines);
            context.AddIntegrationMappings(
              new IntegrationMappingDescriptor {
                IntegrationDocumentNumber = document.IntegrationDocumentNumber,
                IntegrationDocumentType = integrationDocumentType,
                TransactionLines = document.TransactionLines
              });
            context.SaveChanges();
          });
    }

  }

}