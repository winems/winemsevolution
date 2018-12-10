using CSharpFunctionalExtensions;
using Pastel.Evolution;
using RadiusCSharp.Core.Strings;
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

            var salesOrder = new SalesOrder {
              Customer = customer,
              DeliverTo = new Address(
                customer.PhysicalAddress.Line1.EmptyIfNull(),
                customer.PhysicalAddress.Line2.EmptyIfNull(),
                customer.PhysicalAddress.Line3.EmptyIfNull(),
                customer.PhysicalAddress.Line4.EmptyIfNull(),
                customer.PhysicalAddress.Line5.EmptyIfNull(),
                customer.PhysicalAddress.Line6.EmptyIfNull()),
              DeliveryDate = salesOrderTransactionDocument.TransactionDate,
              Description = "Tax Invoice",
              DiscountPercent = (double) salesOrderTransactionDocument.DocumentDiscountPercentage,
              DueDate = salesOrderTransactionDocument.TransactionDate,
              InvoiceTo = new Address(
                customer.PostalAddress.Line1.EmptyIfNull(),
                customer.PostalAddress.Line2.EmptyIfNull(),
                customer.PostalAddress.Line3.EmptyIfNull(),
                customer.PostalAddress.Line4.EmptyIfNull(),
                customer.PostalAddress.Line5.EmptyIfNull(),
                customer.PostalAddress.Line6.EmptyIfNull()),
              MessageLine1 = salesOrderTransactionDocument.MessageLine1,
              MessageLine2 = salesOrderTransactionDocument.MessageLine2,
              MessageLine3 = salesOrderTransactionDocument.MessageLine3,
              OrderDate = salesOrderTransactionDocument.TransactionDate,
              OrderNo = salesOrderTransactionDocument.DocumentNumber,
              TaxMode = customer.IsForeignCurrencyAccount ? TaxMode.Exclusive : TaxMode.Inclusive
            };

            if (customer.IsForeignCurrencyAccount && salesOrderTransactionDocument.ExchangeRate > 0)
              salesOrder.ExchangeRate = (double) salesOrderTransactionDocument.ExchangeRate;

            return Result.Ok(
              salesOrder);
          });

  }

}