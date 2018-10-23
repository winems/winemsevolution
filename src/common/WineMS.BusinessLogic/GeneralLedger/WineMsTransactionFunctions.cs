using System;
using System.Threading;
using WineMS.Common;

namespace WineMS.BusinessLogic.GeneralLedger {

  public static class WineMsTransactionFunctions {

    public static void ProcessGeneralLedgerTransactions(IBackgroundWorker backgroundWorker)
    {
      // Priority 3

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

    public static void ProcessSalesOrderTransactions(IBackgroundWorker backgroundWorker)
    {
      // Priority 1

      /*
       * Same process as PO.
       * - Transaction types: INTERNALSALES
       * - Need to process in multi-currency.
       * - Empty currency is Home currency.
       *
       */
    }

    public static void ProcessStockTransactions(IBackgroundWorker backgroundWorker) { }

    public static void ProcessPurchaseOrderTransactions(IBackgroundWorker backgroundWorker)
    {

      // Priority 2

      /*
       * 1. Purchase Order generated and approved in WineMS
       * 2. Once goods/non-stock received at winery a WineMS goods received is issued and completed
       * 3. On completed - a job runs that adds a 'GOODSRECEIVED', GRAPERECEIVAL, WINERECEIVAL entry to the buffer table with 'Posted' = 0
       *
       * 4. Evolution 'picks' unposted entries and generates a Purchase Order.
       *    - Group by WineMsTransaction.DocumentNumber (One PO for each of these.)
       *    - Use WineMsTransaction.DocumentNumber as the PO number in Evolution.
       *    - Use WineMsTransaction.TransactionDate as the PO and GRV date.
       *    - TransactionAmountExVat and Quantity are line values.
       *    - Create GL Lines on PO.
       *    - Use WineMsTransaction.AccountCode as the account code on the line.
       *    - Use WineMsTransaction.CustomerSupplierAccountCode as the supplier account code.
       *    - Use WineMsTransaction.CompanyId refers to the Evolution company database the transaction belongs to.
       *    - Record WineMsTransaction.Guid together with WineMsTransaction.DocumentNumber for later use when setting
       *      WineMsTransaction.CompletelyInvoiced
       * 5. Once posted set the Posted = 1
       *    
       *
       */

    }

  }

}