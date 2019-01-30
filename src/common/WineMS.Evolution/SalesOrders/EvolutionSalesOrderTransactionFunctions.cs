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
          order => order.AddSalesOrderLines(wineMsSalesOrderTransactionDocument))
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
          () =>
          {
            var customer = new Customer(salesOrderTransactionDocument.CustomerAccountCode);

            var salesOrder = (SalesOrder)
                new SalesOrder {
                    Customer = customer,
                    DeliveryDate = salesOrderTransactionDocument.TransactionDate,
                    Description = "Tax Invoice",
                    DiscountPercent = (double) salesOrderTransactionDocument.DocumentDiscountPercentage,
                    DueDate = salesOrderTransactionDocument.TransactionDate,
                    InvoiceDate = salesOrderTransactionDocument.TransactionDate,
                    OrderDate = salesOrderTransactionDocument.TransactionDate,
                    OrderNo = salesOrderTransactionDocument.DocumentNumber
                  }
                  .SetDeliveryAddress(customer)
                  .SetPostalAddress(customer)
                  .SetMessageLines(salesOrderTransactionDocument)
                  .SetTaxMode(customer)
                  .SetExchangeRate(customer, salesOrderTransactionDocument.ExchangeRate)
              ;

            return Result.Ok(salesOrder);
          });

  }


  

}