using WineMS.Common;
using WineMS.Common.Constants;

namespace WineMS.BusinessLogic.PurchaseOrders {

  public static class WineMsPurchaseOrdersTransactionFunctions {

    public static void Execute(IBackgroundWorker backgroundWorker)
    {
      var transactionTypes = new[] {
        WineMsTransactionTypes.GoodsReceived,
        WineMsTransactionTypes.GrapeReceival,
        WineMsTransactionTypes.WineReceival
      };


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