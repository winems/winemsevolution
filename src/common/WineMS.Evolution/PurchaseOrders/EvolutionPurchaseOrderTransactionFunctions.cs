using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.PurchaseOrders {

  public static class EvolutionPurchaseOrderTransactionFunctions {

    public static Result<WineMsPurchaseOrderTransactionDocument> ProcessTransaction(
      WineMsPurchaseOrderTransactionDocument wineMsSalesOrderTransactionDocument) =>
      CreatePurchaseOrder(wineMsSalesOrderTransactionDocument)
        .OnSuccess(
          order => order.AddOrderLines(wineMsSalesOrderTransactionDocument))
        .OnSuccess(
          order => ExceptionWrapper
            .Wrap(
              () =>
              {
                order.Save();
                wineMsSalesOrderTransactionDocument.IntegrationDocumentNumber = order.OrderNo;
                return Result.Ok(wineMsSalesOrderTransactionDocument);
              }));

    private static Result<PurchaseOrder> CreatePurchaseOrder(
      WineMsPurchaseOrderTransactionDocument salesOrderTransactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new PurchaseOrder {
              Supplier = new Supplier(salesOrderTransactionDocument.SupplierAccountCode),
              DeliveryDate = salesOrderTransactionDocument.TransactionDate,
              DueDate = salesOrderTransactionDocument.TransactionDate,
              OrderDate = salesOrderTransactionDocument.TransactionDate,
              OrderNo = salesOrderTransactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));

  }

}