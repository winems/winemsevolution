using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Configuration;
using WineMS.Common.Constants;
using WineMS.Evolution.PurchaseOrders;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.PurchaseOrders {

  public static class WineMsPurchaseOrdersTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsPurchaseOrderTransactions(),
          wineMsTransactionDocument =>
            EvolutionPurchaseOrderTransactionFunctions
              .ProcessTransaction((WineMsPurchaseOrderTransactionDocument)wineMsTransactionDocument, SystemConfiguration.PurchaseOrderIntegrationType())
              .Tap(document => { document.CompletePosting(IntegrationDocumentTypes.PurchaseOrder); }));
  }
}