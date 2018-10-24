using WineMS.BusinessLogic.GeneralLedger;
using WineMS.BusinessLogic.PurchaseOrders;
using WineMS.BusinessLogic.SalesOrders;
using WineMS.Common;

namespace WineMS.BusinessLogic.Extensions {

  public static class WineMsTransactionFunctions {
    
    public static void ProcessGeneralLedgerTransactions(IBackgroundWorker backgroundWorker)
    {
      // Priority 3
      WineMsGeneralLedgerTransactionFunctions.Execute(backgroundWorker);
    }

    public static void ProcessSalesOrderTransactions(IBackgroundWorker backgroundWorker)
    {
      WineMsSalesOrdersTransactionFunctions.Execute(backgroundWorker);
    }

    public static void ProcessStockTransactions(IBackgroundWorker backgroundWorker) { }

    public static void ProcessPurchaseOrderTransactions(IBackgroundWorker backgroundWorker)
    {
      // Priority 2
      WineMsPurchaseOrdersTransactionFunctions.Execute(backgroundWorker);
    }

  }

}