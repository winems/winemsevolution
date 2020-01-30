using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.PurchaseOrders {

  public static class EvolutionReturnToSupplierTransactionFunctions {

    public static Result<WineMsReturnToSupplierTransactionDocument> ProcessTransaction(
      WineMsReturnToSupplierTransactionDocument wineMsReturnToSupplierTransactionDocument) =>
      CreateReturnToSupplier(wineMsReturnToSupplierTransactionDocument)
        .Bind(
          order => order.AddReturnToSupplierLines(wineMsReturnToSupplierTransactionDocument))
        .Bind(
          order => ExceptionWrapper
            .Wrap(
              () => {
                order.Complete();
                wineMsReturnToSupplierTransactionDocument.IntegrationDocumentNumber = order.OrderNo;
                return Result.Ok(wineMsReturnToSupplierTransactionDocument);
              }));

    private static Result<ReturnToSupplier> CreateReturnToSupplier(
      WineMsReturnToSupplierTransactionDocument transactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new ReturnToSupplier {
              Supplier = new Supplier(transactionDocument.SupplierAccountCode),
              DeliveryDate = transactionDocument.TransactionDate,
              DueDate = transactionDocument.TransactionDate,
              OrderDate = transactionDocument.TransactionDate,
              OrderNo = transactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));

  }

}