﻿using System;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
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
                  var orderLine = new OrderDetail();
                  order.Detail.Add(orderLine);

                  orderLine.TaxMode = TaxMode.Exclusive;
                  orderLine.GLAccount = new GLAccount(transactionLine.GeneralLedgerAccountCode);
                  orderLine.Quantity = (double) transactionLine.Quantity;
                  orderLine.ToProcess = orderLine.Quantity;

                  var unitSellingPrice =
                    (double) transactionLine.TransactionAmountExVat /
                    (Math.Abs((double) transactionLine.Quantity) > 0.00
                      ? (double) transactionLine.Quantity
                      : 1);

                  if (transactionLine.CurrencyCode.IsNullOrWhiteSpace())
                    orderLine.UnitSellingPrice = unitSellingPrice;
                  else
                    orderLine.UnitSellingPriceForeign = unitSellingPrice;

                  orderLine.TaxType = new TaxRate(transactionLine.TaxTypeId);
                  orderLine.Description = transactionLine.Description1;

                  return Result.Ok();
                }),
          salesOrderTransactionDocument.TransactionLines)
        .OnSuccess(() => order);

  }

}