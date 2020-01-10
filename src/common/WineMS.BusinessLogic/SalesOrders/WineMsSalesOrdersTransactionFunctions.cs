using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Configuration;
using WineMS.Common.Constants;
using WineMS.Evolution.SalesOrders;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.SalesOrders {

  public static class WineMsSalesOrdersTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsSalesOrderTransactions(),
          wineMsTransactionDocument =>
            EvolutionSalesOrderTransactionFunctions
              .ProcessTransaction((WineMsSalesOrderTransactionDocument) wineMsTransactionDocument, SystemConfiguration.GetSalesOrderOptions())
              .Tap(
                document => { document.CompletePosting(IntegrationDocumentTypes.SalesOrder); }));

  }

}