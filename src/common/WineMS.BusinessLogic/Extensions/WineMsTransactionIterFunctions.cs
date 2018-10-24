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
      this string[] transactionTypes,
      IBackgroundWorker backgroundWorker,
      Func<WineMsTransactionDocument, Result> func)
    {
      return transactionTypes
        .ForEachNewTransaction(
          backgroundWorker,
          transaction =>
            transaction
              .CompanyId
              .GetEvolutionConnectionStrings()
              .WrapInEvolutionSdk(transaction.BranchId, () => func(transaction)));
    }

    private static Result ForEachNewTransaction(
      this string[] transactionTypes,
      IBackgroundWorker backgroundWorker,
      Func<WineMsTransactionDocument, Result> func)
    {
      return WineMsDbContextFunctions
             .WrapInDbContext(context => context.ListNewWineMsTransactions(transactionTypes))
             .ForEachNewTransaction(backgroundWorker, func);
    }

    private static Result ForEachNewTransaction(
      this WineMsTransactionDocument[] transactions,
      IBackgroundWorker backgroundWorker,
      Func<WineMsTransactionDocument, Result> func)
    {
      var errors = new StringBuilder();
      var count = 0;
      var maxCount = transactions.Length;
      foreach (var transaction in transactions) {
        func(transaction)
          .OnFailure(err => errors.AppendLine(err));
        var percentProgress = ProgressReportFunctions.CalcPercentProgress(++count, maxCount);
        backgroundWorker.ReportProgress(percentProgress);
      }

      return errors.Length == 0 ? Result.Ok() : Result.Fail(errors.ToString());
    }

  }

}