using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.SalesOrders;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.SalesOrders {

  public static class WineMsSalesOrdersTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker)
    {
      var transactionTypes = new[] {WineMsTransactionTypes.SalesOrders};

      return transactionTypes
        .ForEachNewTransactionEvolutionContext(
          backgroundWorker,
          wineMsTransactionDocument =>
            EvolutionSalesOrderTransactionFunctions
              .ProcessTransaction(wineMsTransactionDocument)
              .OnSuccess(
                document => { document.CompletePosting(IntegrationDocumentTypes.SalesOrder); }));
    }

  }

}