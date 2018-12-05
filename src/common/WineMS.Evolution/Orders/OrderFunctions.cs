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

    public static Result<OrderBase> AddOrderLines(
      this OrderBase order,
      WineMsOrderTransactionDocument salesOrderTransactionDocument) =>
      WineMsTransactionDocumentFunctions
        .ForEachTransactionDocumentLine(
          transactionLine =>
            ExceptionWrapper
              .Wrap(
                () =>
                {
                  var isGeneralLedgerLine = IsGeneralLedgerLine(transactionLine);

                  var orderLine = new OrderDetail();
                  order.Detail.Add(orderLine);

                  orderLine.TaxMode = order.TaxMode;

                  if (isGeneralLedgerLine)
                    orderLine.GLAccount = new GLAccount(transactionLine.GeneralLedgerItemCode);
                  else {
                    orderLine.InventoryItem = new InventoryItem(transactionLine.GeneralLedgerItemCode);
                    if (!transactionLine.WarehouseCode.IsNullOrWhiteSpace())
                      orderLine.Warehouse = new Warehouse(transactionLine.WarehouseCode);
                  }

                  orderLine.Quantity = (double) transactionLine.Quantity;
                  orderLine.ToProcess = orderLine.Quantity;

                  var transactionAmount =
                    (double) (orderLine.TaxMode == TaxMode.Exclusive ? transactionLine.TransactionAmountExVat : transactionLine.TransactionAmountInVat);

                  var unitSellingPrice =
                    transactionAmount /
                    (Math.Abs((double) transactionLine.Quantity) > 0.00
                      ? (double) transactionLine.Quantity
                      : 1);

                  if (transactionLine.CurrencyCode.IsNullOrWhiteSpace())
                    orderLine.UnitSellingPrice = unitSellingPrice;
                  else
                    orderLine.UnitSellingPriceForeign = unitSellingPrice;

                  if (transactionLine.TaxTypeId > 0) {
                    var result = GetOrderLineTaxType(transactionLine);
                    if (result.IsFailure)
                      return Result.Fail(result.Error);
                    orderLine.TaxType = result.Value;
                  }

                  orderLine.DiscountPercent = (double) transactionLine.LineDiscountPercentage;
                  orderLine.Description = transactionLine.Description1;

                  return Result.Ok();
                }),
          salesOrderTransactionDocument.TransactionLines)
        .OnSuccess(() => order);

    private static bool IsGeneralLedgerLine(IWineMsTransactionLine transactionLine) =>
      transactionLine.LineType.IsNullOrWhiteSpace() || transactionLine.LineType.ToUpper() == "GL";

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

  }

}