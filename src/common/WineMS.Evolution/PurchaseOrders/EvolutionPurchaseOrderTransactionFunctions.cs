using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.PurchaseOrders {

  public static class EvolutionPurchaseOrderTransactionFunctions {

    public static Result<WineMsTransactionDocument> ProcessTransaction(
      WineMsTransactionDocument wineMsTransactionDocument) =>
      CreatePurchaseOrder(wineMsTransactionDocument)
        .OnSuccess(
          order => order.AddOrderLines(wineMsTransactionDocument))
        .OnSuccess(
          order => ExceptionWrapper
            .Wrap(
              () =>
              {
                order.Save();
                wineMsTransactionDocument.IntegrationDocumentNumber = order.OrderNo;
                return Result.Ok(wineMsTransactionDocument);
              }));

    private static Result<PurchaseOrder> CreatePurchaseOrder(
      WineMsTransactionDocument transactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new PurchaseOrder {
              Supplier = new Supplier(transactionDocument.CustomerSupplierAccountCode),
              DeliveryDate = transactionDocument.TransactionDate,
              DueDate = transactionDocument.TransactionDate,
              OrderDate = transactionDocument.TransactionDate,
              OrderNo = transactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));

  }

}