using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.WineMS.DataAccess;

namespace WineMS.BusinessLogic.SalesOrders {

  public static class WineMsSalesOrdersTransactionFunctions {

    public static void Execute(IBackgroundWorker backgroundWorker)
    {
      var transactionTypes = new[] { WineMsTransactionTypes.SalesOrders };
      transactionTypes
        .ForEachNewTransactionEvolutionContext(backgroundWorker, ProcessTransaction);
    }

    private static Result ProcessTransaction(WineMsTransactionDocument transaction)
    {
      // TODO: create sales order.
      // TODO: populate sales order.
      // TODO: complete sales order.
      /*
       * Same process as PO.
       * - Transaction types: INTERNALSALES
       * - Need to process in multi-currency.
       * - Empty currency is Home currency.
       *
       */
      return Result.Ok();
    }

  }

}