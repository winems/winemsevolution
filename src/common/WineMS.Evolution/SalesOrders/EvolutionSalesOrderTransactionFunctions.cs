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
          () =>
          {
            var customer = new Customer(salesOrderTransactionDocument.CustomerAccountCode);

            return Result.Ok(
              new SalesOrder {
                Customer = customer,
                DeliverTo = new Address(
                  customer.PhysicalAddress.Line1,
                  customer.PhysicalAddress.Line2,
                  customer.PhysicalAddress.Line3,
                  customer.PhysicalAddress.Line4,
                  customer.PhysicalAddress.Line5,
                  customer.PhysicalAddress.Line6),
                DeliveryDate = salesOrderTransactionDocument.TransactionDate,
                Description = "Tax Invoice",
                DiscountPercent = (double) salesOrderTransactionDocument.DocumentDiscountPercentage,
                DueDate = salesOrderTransactionDocument.TransactionDate,
                InvoiceTo = new Address(
                  customer.PostalAddress.Line1,
                  customer.PostalAddress.Line2,
                  customer.PostalAddress.Line3,
                  customer.PostalAddress.Line4,
                  customer.PostalAddress.Line5,
                  customer.PostalAddress.Line6),
                OrderDate = salesOrderTransactionDocument.TransactionDate,
                OrderNo = salesOrderTransactionDocument.DocumentNumber,
                TaxMode = customer.IsForeignCurrencyAccount ? TaxMode.Exclusive : TaxMode.Inclusive
              });
          });

  }

}