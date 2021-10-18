using System;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Constants;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.PurchaseOrders {

  public static class EvolutionReturnToSupplierTransactionFunctions {

    public static Result<WineMsReturnToSupplierTransactionDocument> ProcessTransaction(
      WineMsReturnToSupplierTransactionDocument wineMsReturnToSupplierTransactionDocument,
      ReturnToSupplierIntegrationType returnToSupplierIntegrationType) =>
      CreateReturnToSupplier(wineMsReturnToSupplierTransactionDocument)
        .Bind(order => order.AddReturnToSupplierLines(wineMsReturnToSupplierTransactionDocument))
        .Bind(order => ExceptionWrapper
        .Wrap(() => {
          switch (returnToSupplierIntegrationType) {
            case ReturnToSupplierIntegrationType.Post:
              order.InvoiceDate = order.DeliveryDate;
              order.Complete();
              break;

            case ReturnToSupplierIntegrationType.SaveOnly:
              order.Save();
              break;

            default:
              throw new ArgumentOutOfRangeException(nameof(returnToSupplierIntegrationType), returnToSupplierIntegrationType, null);
          }
          wineMsReturnToSupplierTransactionDocument.IntegrationDocumentNumber = order.OrderNo;
          return Result.Ok(wineMsReturnToSupplierTransactionDocument);
        }));

    private static Result<ReturnToSupplier> CreateReturnToSupplier(WineMsReturnToSupplierTransactionDocument transactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new ReturnToSupplier {
              Supplier = new Supplier(transactionDocument.SupplierAccountCode),
              ExternalOrderNo = transactionDocument.SupplierInvoiceNumber,
              DeliveryDate = transactionDocument.TransactionDate,
              DueDate = transactionDocument.TransactionDate,
              ExchangeRate = (double)transactionDocument.ExchangeRate,
              OrderDate = transactionDocument.TransactionDate,
              OrderNo = transactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));
  }
}