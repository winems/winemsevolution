using System;
using System.Threading;
using WineMS.Common;

namespace WineMS.BusinessLogic.GeneralLedger {

  public static class WineMsTransactionFunctions {

    public static void ProcessGeneralLedgerTransactions(ICancelableBackgroundWorker cancelableBackgroundWorker)
    {
      var start = DateTime.Now;
      while (true) {
        Thread.Sleep(100);
        if (DateTime.Now.TimeOfDay.TotalSeconds - start.TimeOfDay.TotalSeconds > 30 ||
            cancelableBackgroundWorker.CancellationPending) break;
      }
    }

    public static void ProcessStockTransactions(ICancelableBackgroundWorker cancelableBackgroundWorker) { }
    public static void ProcessPurchaseOrderTransactions(ICancelableBackgroundWorker cancelableBackgroundWorker) { }

  }

}