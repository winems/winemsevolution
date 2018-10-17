using System;
using System.Threading;
using WineMS.Common;

namespace WineMS.BusinessLogic.GeneralLedger {

  public static class WineMsTransactionFunctions {

    public static void ProcessGeneralLedgerTransactions(IBackgroundWorker backgroundWorker)
    {
      var start = DateTime.Now;
      while (true) {
        Thread.Sleep(100);
        var seconds = DateTime.Now.TimeOfDay.TotalSeconds - start.TimeOfDay.TotalSeconds;
        if (seconds > 30 ||
            backgroundWorker.CancellationPending) break;
        var percentProgress = (int) ((seconds / 30) * 100);
        backgroundWorker.ReportProgress(percentProgress);
      }
    }

    public static void ProcessStockTransactions(IBackgroundWorker backgroundWorker) { }
    public static void ProcessPurchaseOrderTransactions(IBackgroundWorker backgroundWorker) { }

  }

}