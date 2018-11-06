using System;
using System.Text;
using CSharpFunctionalExtensions;
using WineMS.Common;
using WineMS.Common.Configuration;
using WineMS.Evolution;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.Extensions {

  public static class WineMsTransactionIterFunctions {

    public static Result ForEachNewTransactionEvolutionContext(
      this IBackgroundWorker backgroundWorker,
      Func<WineMsDbContext, WineMsOrderTransactionDocument[]> loadData,
      Func<WineMsOrderTransactionDocument, Result> func)
    {
      return backgroundWorker
        .ForEachNewTransaction(loadData,
          transaction =>
            transaction
              .CompanyId
              .GetEvolutionConnectionStrings()
              .WrapInEvolutionSdk(transaction.BranchId, () => func(transaction)));
    }

    private static Result ForEachNewTransaction(
      this IBackgroundWorker backgroundWorker,
      Func<WineMsDbContext, WineMsOrderTransactionDocument[]> loadData,
      Func<WineMsOrderTransactionDocument, Result> func) =>
      WineMsDbContextFunctions
        .WrapInDbContext(loadData)
        .ForEachNewTransaction(backgroundWorker, func);

    private static Result ForEachNewTransaction(
      this WineMsOrderTransactionDocument[] orderTransactions,
      IBackgroundWorker backgroundWorker,
      Func<WineMsOrderTransactionDocument, Result> func)
    {
      var errors = new StringBuilder();
      var count = 0;
      var maxCount = orderTransactions.Length;
      foreach (var transaction in orderTransactions) {
        func(transaction)
          .OnFailure(err => errors.AppendLine(err));
        var percentProgress = ProgressReportFunctions.CalcPercentProgress(++count, maxCount);
        backgroundWorker.ReportProgress(percentProgress);
      }

      return errors.Length == 0 ? Result.Ok() : Result.Fail(errors.ToString());
    }

  }

}