using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Evolution.Stock;
using WineMS.WineMS.DataAccess;

namespace WineMS.BusinessLogic.Stock {

  public static class WineMsStockJournalTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsStockJournalTransactions(),
          wineMsTransactionDocument =>
            EvolutionStockJournalFunctions
              .ProcessTransaction((WineMsStockJournalTransactionBatch) wineMsTransactionDocument)
              .Tap(
                document => {
                  /*document.CompletePosting(IntegrationDocumentTypes.PurchaseOrder);*/
                }));

  }

}