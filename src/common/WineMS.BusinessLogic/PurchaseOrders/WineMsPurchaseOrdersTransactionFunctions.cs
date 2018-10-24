using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.PurchaseOrders;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.PurchaseOrders {

  public static class WineMsPurchaseOrdersTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker)
    {
      var transactionTypes = new[] {
        WineMsTransactionTypes.GoodsReceived,
        WineMsTransactionTypes.GrapeReceival,
        WineMsTransactionTypes.WineReceival
      };

      return transactionTypes
        .ForEachNewTransactionEvolutionContext(
          backgroundWorker,
          wineMsTransactionDocument =>
            EvolutionPurchaseOrderTransactionFunctions
              .ProcessTransaction(wineMsTransactionDocument)
              .OnSuccess(
                document => { document.CompletePosting(IntegrationDocumentTypes.PurchaseOrder); }));
    }

  }

}