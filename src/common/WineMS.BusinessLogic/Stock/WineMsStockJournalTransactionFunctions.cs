using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.Stock;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.Stock {

  public static class WineMsStockJournalTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsStockJournalTransactions(),
          wineMsTransactionDocument =>
            EvolutionStockJournalTransactionFunctions
              .ProcessTransaction((WineMsStockJournalTransactionBatch) wineMsTransactionDocument)
              .Tap(batch => { batch.CompletePosting(IntegrationDocumentTypes.StockJournal); }));

  }

}