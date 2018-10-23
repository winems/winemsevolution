using System;
using CSharpFunctionalExtensions;
using WineMS.Common;
using WineMS.Common.Configuration;
using WineMS.Evolution;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.Extensions {

  public static class WineMsTransactionIterFunctions {

    public static void ForEachNewTransactionEvolutionContext(
      this string[] transactionTypes,
      IBackgroundWorker backgroundWorker,
      Func<WineMsTransactionDocument, Result> func)
    {
      transactionTypes
        .ForEachNewTransaction(
          backgroundWorker,
          transaction =>
          {
            transaction
              .CompanyId
              .GetEvolutionConnectionStrings()
              .WrapInEvolutionSdk(transaction.BranchId, () => func(transaction));
          });
    }

    private static void ForEachNewTransaction(
      this string[] transactionTypes,
      IBackgroundWorker backgroundWorker,
      Action<WineMsTransactionDocument> action)
    {
      WineMsDbContextFunctions
        .WrapInDbContext(context => context.ListNewWineMsTransactions(transactionTypes))
        .ForEachNewTransaction(backgroundWorker, action);
    }

    private static void ForEachNewTransaction(
      this WineMsTransactionDocument[] transactions,
      IBackgroundWorker backgroundWorker,
      Action<WineMsTransactionDocument> action)
    {
      var count = 0;
      var maxCount = transactions.Length;
      foreach (var transaction in transactions) {
        action(transaction);
        var percentProgress = ProgressReportFunctions.CalcPercentProgress(++count, maxCount);
        backgroundWorker.ReportProgress(percentProgress);
      }
    }

  }

}