using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Configuration;
using WineMS.Common.Constants;
using WineMS.Evolution.PurchaseOrders;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.ReturnToSupplier {

  public static class WineMsReturnToSupplierTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsReturnToSupplierTransactions(),
          wineMsTransactionDocument =>
            EvolutionReturnToSupplierTransactionFunctions
              .ProcessTransaction((WineMsReturnToSupplierTransactionDocument) wineMsTransactionDocument,
                SystemConfiguration.ReturnToSupplierIntegrationType())
              .Tap(
                document => { document.CompletePosting(IntegrationDocumentTypes.ReturnToSupplierPost); }));

  }

}