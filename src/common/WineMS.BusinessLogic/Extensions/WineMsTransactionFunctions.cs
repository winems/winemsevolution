using CSharpFunctionalExtensions;
using RadiusCSharp.Core.Logging;
using WineMS.BusinessLogic.CreditNotes;
using WineMS.BusinessLogic.GeneralLedger;
using WineMS.BusinessLogic.PurchaseOrders;
using WineMS.BusinessLogic.SalesOrders;
using WineMS.Common;

namespace WineMS.BusinessLogic.Extensions {

  public static class WineMsTransactionFunctions {

    public static Result ProcessCreditNoteTransactions(IBackgroundWorker backgroundWorker) =>
      WineMsCreditNotesTransactionFunctions
        .Execute(backgroundWorker)
        .OnFailure(error => error.LogException());

    public static Result ProcessGeneralLedgerTransactions(IBackgroundWorker backgroundWorker) =>
      WineMsGeneralLedgerJournalTransactionFunctions
        .Execute(backgroundWorker)
        .OnFailure(error => error.LogException());

    public static Result ProcessPurchaseOrderTransactions(IBackgroundWorker backgroundWorker) =>
      WineMsPurchaseOrdersTransactionFunctions
        .Execute(backgroundWorker)
        .OnFailure(error => error.LogException());

    public static Result ProcessReturnToSupplierTransactions(IBackgroundWorker backgroundWorker) {
      "Return to supplier not implemented.".LogException();
      return Result.Fail("Return to supplier not implemented.");
    }

    public static Result ProcessSalesOrderTransactions(IBackgroundWorker backgroundWorker) =>
      WineMsSalesOrdersTransactionFunctions
        .Execute(backgroundWorker)
        .OnFailure(error => error.LogException());

  }

}