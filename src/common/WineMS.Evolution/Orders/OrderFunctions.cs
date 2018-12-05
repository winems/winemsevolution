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

    private static Result<OrderBase> AddOrderLines(
      this OrderBase order,
      WineMsOrderTransactionDocument salesOrderTransactionDocument,
      OrderTransactionType orderTransactionType) =>
      WineMsTransactionDocumentFunctions
        .ForEachTransactionDocumentLine(
          transactionLine =>
            ExceptionWrapper
              .Wrap(
                () =>
                {
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

                  SetUserDefinedFields(orderTransactionType, orderLine, transactionLine);

                  return Result.Ok();
                }),
          salesOrderTransactionDocument.TransactionLines)
        .OnSuccess(() => order);

    private static OrderDetail NewOrderDetail(OrderBase order)
    {
      var orderLine = new OrderDetail();
      order.Detail.Add(orderLine);
      orderLine.TaxMode = order.TaxMode;
      return orderLine;
    }

    private static bool IsGeneralLedgerLine(IWineMsTransactionLine transactionLine) =>
      transactionLine.LineType.IsNullOrWhiteSpace() || transactionLine.LineType.ToUpper() == "GL";

    private static void SetGeneralLedgerAccount(OrderDetail orderLine, IWineMsTransactionLine transactionLine)
    {
      orderLine.GLAccount = new GLAccount(transactionLine.GeneralLedgerItemCode);
    }

    private static void SetInventoryItem(OrderDetail orderLine, IWineMsTransactionLine transactionLine)
    {
      orderLine.InventoryItem = new InventoryItem(transactionLine.GeneralLedgerItemCode);
      if (!transactionLine.WarehouseCode.IsNullOrWhiteSpace())
        orderLine.Warehouse = new Warehouse(transactionLine.WarehouseCode);
    }

    private static void SetUnitSellingPrice(OrderDetail orderLine, IWineMsTransactionLine transactionLine)
    {
      var transactionAmount = GetTransactionAmount(orderLine, transactionLine);

      var unitSellingPrice =
        transactionAmount /
        (Math.Abs((double) transactionLine.Quantity) > 0.00
          ? (double) transactionLine.Quantity
          : 1);

      if (transactionLine.CurrencyCode.IsNullOrWhiteSpace())
        orderLine.UnitSellingPrice = unitSellingPrice;
      else
        orderLine.UnitSellingPriceForeign = unitSellingPrice;
    }

    private static double GetTransactionAmount(OrderDetail orderLine, IWineMsTransactionLine transactionLine) =>
      (double) (orderLine.TaxMode == TaxMode.Exclusive ? transactionLine.TransactionAmountExVat : transactionLine.TransactionAmountInVat);

    private static Result<TaxRate> GetOrderLineTaxType(IWineMsTransactionLine transactionLine)
    {
      try {
        return Result.Ok(new TaxRate(transactionLine.TaxTypeId));
      }
      catch (Exception e) {
        e.LogException();
        return Result.Fail<TaxRate>(e.GetExceptionMessages());
      }
    }

    private static void SetUserDefinedFields(OrderTransactionType orderTransactionType, OrderDetail orderLine, IWineMsTransactionLine transactionLine)
    {
      switch (orderTransactionType) {
        case OrderTransactionType.SalesOrder:
          orderLine.SetUserField("ucIDSOrdTxCMwineMSGuid", $"{transactionLine.Guid}");
          break;
        case OrderTransactionType.PurchaseOrder:
          orderLine.SetUserField("ucIDPOrdTxCMwineMSGuid", $"{transactionLine.Guid}");
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(orderTransactionType), orderTransactionType, null);
      }
    }

  }

  public enum OrderTransactionType {

    SalesOrder,
    PurchaseOrder

  }

}