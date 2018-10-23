using System;
using System.Threading;
using WineMS.Common;

namespace WineMS.BusinessLogic.GeneralLedger {

  public static class WineMsGeneralLedgerTransactionFunctions {

    public static void Execute(IBackgroundWorker backgroundWorker)
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

  }

}