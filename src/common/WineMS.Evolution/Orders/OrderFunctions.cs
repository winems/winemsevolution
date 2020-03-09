using System;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using RadiusCSharp.Core.Logging;
using RadiusCSharp.Core.Strings;
using WineMS.Common.Extensions;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.Evolution.Orders {

  public static class OrderFunctions {

    public static Result<OrderBase> AddSalesOrderLines(
      this OrderBase order,
      WineMsOrderTransactionDocument salesOrderTransactionDocument) =>
      order.AddOrderLines(salesOrderTransactionDocument, OrderTransactionType.SalesOrder);

    public static Result<OrderBase> AddPurchaseOrderLines(
      this OrderBase order,
      WineMsOrderTransactionDocument salesOrderTransactionDocument) =>
      order.AddOrderLines(salesOrderTransactionDocument, OrderTransactionType.PurchaseOrder);

    public static Result<OrderBase> AddReturnToSupplierLines(
      this OrderBase order,
      WineMsOrderTransactionDocument salesOrderTransactionDocument) =>
      order.AddOrderLines(salesOrderTransactionDocument, OrderTransactionType.ReturnToSupplier);

    public static OrderBase SetDeliveryAddress(this OrderBase order, Customer customer) {
      order.DeliverTo = new Address(
        customer.PhysicalAddress.Line1.EmptyIfNull(),
        customer.PhysicalAddress.Line2.EmptyIfNull(),
        customer.PhysicalAddress.Line3.EmptyIfNull(),
        customer.PhysicalAddress.Line4.EmptyIfNull(),
        customer.PhysicalAddress.Line5.EmptyIfNull(),
        customer.PhysicalAddress.Line6.EmptyIfNull());
      return order;
    }

    public static OrderBase SetPostalAddress(this OrderBase order, Customer customer) {
      order.InvoiceTo = new Address(
        customer.PostalAddress.Line1.EmptyIfNull(),
        customer.PostalAddress.Line2.EmptyIfNull(),
        customer.PostalAddress.Line3.EmptyIfNull(),
        customer.PostalAddress.Line4.EmptyIfNull(),
        customer.PostalAddress.Line5.EmptyIfNull(),
        customer.PostalAddress.Line6.EmptyIfNull());
      return order;
    }

    public static OrderBase SetTaxMode(this OrderBase order, Customer customer) {
      order.TaxMode = customer.IsForeignCurrencyAccount ? TaxMode.Exclusive : TaxMode.Inclusive;
      return order;
    }

    public static OrderBase SetExchangeRate(this OrderBase order, Customer customer, decimal exchangeRate) {
      if (customer.IsForeignCurrencyAccount && exchangeRate > 0)
        order.ExchangeRate = (double) exchangeRate;
      return order;
    }

    public static OrderBase SetMessageLines(this OrderBase order, IOrderMessageLines orderMessageLines) {
      order.MessageLine1 = orderMessageLines.MessageLine1;
      order.MessageLine2 = orderMessageLines.MessageLine2;
      order.MessageLine3 = orderMessageLines.MessageLine3;
      return order;
    }

    private static Result<OrderBase> AddOrderLines(
      this OrderBase order,
      WineMsOrderTransactionDocument salesOrderTransactionDocument,
      OrderTransactionType orderTransactionType) =>
      WineMsTransactionDocumentFunctions
        .ForEachTransactionDocumentLine(
          transactionLine =>
            ExceptionWrapper
              .Wrap(
                () => {
                  var isGeneralLedgerLine = IsGeneralLedgerLine(transactionLine);
                  var orderLine = NewOrderDetail(order);

                  if (isGeneralLedgerLine)
                    SetGeneralLedgerAccount(orderLine, transactionLine);
                  else
                    SetInventoryItem(orderLine, transactionLine);

                  orderLine.Quantity = (double) transactionLine.Quantity;
                  orderLine.ToProcess = orderLine.Quantity;
                  SetUnitSellingPrice(orderLine, transactionLine);

                  if (transactionLine.TaxTypeId > 0) {
                    var result = GetOrderLineTaxType(transactionLine);
                    if (result.IsFailure)
                      return Result.Fail(result.Error);
                    orderLine.TaxType = result.Value;
                  }

                  orderLine.DiscountPercent = (double) transactionLine.LineDiscountPercentage;
                  orderLine.Description = transactionLine.Description1;

                  if (!transactionLine.ItemNote.IsNullOrWhiteSpace())
                    orderLine.Note = transactionLine.ItemNote;

                  SetUserDefinedFields(orderTransactionType, orderLine, transactionLine);

                  return Result.Ok();
                }),
          salesOrderTransactionDocument.TransactionLines)
        .Map(() => order);

    private static OrderDetail NewOrderDetail(OrderBase order) {
      var orderLine = new OrderDetail {TaxMode = order.TaxMode};
      order.Detail.Add(orderLine);
      return orderLine;
    }

    private static bool IsGeneralLedgerLine(IWineMsTransactionLine transactionLine) =>
      transactionLine.LineType.IsNullOrWhiteSpace() || transactionLine.LineType.ToUpper() == "GL";

    private static void SetGeneralLedgerAccount(OrderDetail orderLine, IWineMsTransactionLine transactionLine) {
      orderLine.GLAccount = new GLAccount(transactionLine.GeneralLedgerItemCode.EmptyIfNull().Trim());
    }

    private static void SetInventoryItem(OrderDetail orderLine, IWineMsTransactionLine transactionLine) {
      orderLine.InventoryItem = new InventoryItem(transactionLine.GeneralLedgerItemCode.EmptyIfNull().Trim());
      if (!transactionLine.WarehouseCode.IsNullOrWhiteSpace())
        orderLine.Warehouse = new Warehouse(transactionLine.WarehouseCode);
    }

    private static void SetUnitSellingPrice(OrderDetail orderLine, IWineMsTransactionLine transactionLine) {
      var transactionAmount = GetTransactionAmount(orderLine, transactionLine);

      var absoluteQuantity = Math.Abs((double) transactionLine.Quantity);

      var unitSellingPrice =
        transactionAmount / (absoluteQuantity > 0.00 ? absoluteQuantity : 1);

      if (transactionLine.CurrencyCode.IsNullOrWhiteSpace())
        orderLine.UnitSellingPrice = unitSellingPrice;
      else
        orderLine.UnitSellingPriceForeign = unitSellingPrice;
    }

    private static double GetTransactionAmount(OrderDetail orderLine, IWineMsTransactionLine transactionLine) =>
      (double) (orderLine.TaxMode == TaxMode.Exclusive ? transactionLine.TransactionAmountExVat : transactionLine.TransactionAmountInVat);

    private static Result<TaxRate> GetOrderLineTaxType(IWineMsTransactionLine transactionLine) {
      try {
        return Result.Ok(new TaxRate(transactionLine.TaxTypeId));
      }
      catch (Exception e) {
        e.LogException();
        return Result.Fail<TaxRate>(e.GetExceptionMessages());
      }
    }

    private static void SetUserDefinedFields(OrderTransactionType orderTransactionType, OrderDetail orderLine, IWineMsTransactionLine transactionLine) {
      switch (orderTransactionType) {
        case OrderTransactionType.SalesOrder:
          orderLine.SetUserField("ucIDSOrdTxCMwineMSGuid", $"{transactionLine.Guid}");
          break;
        case OrderTransactionType.PurchaseOrder:
          orderLine.SetUserField("ucIDPOrdTxCMwineMSGuid", $"{transactionLine.Guid}");
          break;
        case OrderTransactionType.ReturnToSupplier:
          orderLine.SetUserField("ucIDRtsTxCMWineMSGuid", $"{transactionLine.Guid}");
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(orderTransactionType), orderTransactionType, null);
      }
    }

  }

  public enum OrderTransactionType {

    SalesOrder,
    PurchaseOrder,
    ReturnToSupplier

  }

}