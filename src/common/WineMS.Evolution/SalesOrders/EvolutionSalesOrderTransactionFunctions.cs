using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.SalesOrders {

  public static class EvolutionSalesOrderTransactionFunctions {

    public static Result<WineMsSalesOrderTransactionDocument> ProcessTransaction(
      WineMsSalesOrderTransactionDocument wineMsSalesOrderTransactionDocument) =>
      CreateSalesOrder(wineMsSalesOrderTransactionDocument)
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

    private static Result<SalesOrder> CreateSalesOrder(
      WineMsSalesOrderTransactionDocument salesOrderTransactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new SalesOrder {
              Customer = new Customer(salesOrderTransactionDocument.CustomerAccountCode),
              DeliveryDate = salesOrderTransactionDocument.TransactionDate,
              Description = "Tax Invoice",
              DiscountPercent = (double) salesOrderTransactionDocument.DocumentDiscountPercentage,
              DueDate = salesOrderTransactionDocument.TransactionDate,
              OrderDate = salesOrderTransactionDocument.TransactionDate,
              OrderNo = salesOrderTransactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));

  }

}