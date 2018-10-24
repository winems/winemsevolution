using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.SalesOrders {

  public static class EvolutionSalesOrderTransactionFunctions {

    public static Result<WineMsTransactionDocument> ProcessTransaction(
      WineMsTransactionDocument wineMsTransactionDocument) =>
      CreateSalesOrder(wineMsTransactionDocument)
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

    private static Result<SalesOrder> CreateSalesOrder(
      WineMsTransactionDocument transactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new SalesOrder {
              Customer = new Customer(transactionDocument.CustomerSupplierAccountCode),
              DeliveryDate = transactionDocument.TransactionDate,
              DueDate = transactionDocument.TransactionDate,
              OrderDate = transactionDocument.TransactionDate,
              OrderNo = transactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));

  }

}