using System;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Constants;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.PurchaseOrders {

  public static class EvolutionPurchaseOrderTransactionFunctions {

    public static Result<WineMsPurchaseOrderTransactionDocument> ProcessTransaction(
      WineMsPurchaseOrderTransactionDocument wineMsPurchaseOrderTransactionDocument,
      PurchaseOrderIntegrationType purchaseOrderIntegrationType) =>
      CreatePurchaseOrder(wineMsPurchaseOrderTransactionDocument)
        .Bind(order => order.AddPurchaseOrderLines(wineMsPurchaseOrderTransactionDocument))
        .Bind(order => ExceptionWrapper.Wrap(    () => {
          switch (purchaseOrderIntegrationType) {
            case PurchaseOrderIntegrationType.PurchaseOrder:
              order.Save();
              break;

            case PurchaseOrderIntegrationType.GoodsReceivedVoucher:
              var goodsReceivedVoucher = (PurchaseOrder)order;
              goodsReceivedVoucher.InvoiceDate = goodsReceivedVoucher.DeliveryDate;
              goodsReceivedVoucher.CompleteStock();
              break;

            case PurchaseOrderIntegrationType.SupplierInvoice:
              var supplierInvoice = (PurchaseOrder)order;
              supplierInvoice.InvoiceDate = supplierInvoice.DeliveryDate;
              supplierInvoice.Complete();
              break;

            default:
              throw new ArgumentOutOfRangeException();
          }

          wineMsPurchaseOrderTransactionDocument.IntegrationDocumentNumber = order.OrderNo;
          return Result.Ok(wineMsPurchaseOrderTransactionDocument);
        }));

    private static Result<PurchaseOrder> CreatePurchaseOrder(
      WineMsPurchaseOrderTransactionDocument transactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new PurchaseOrder {
              Supplier = new Supplier(transactionDocument.SupplierAccountCode),
              SupplierInvoiceNo = transactionDocument.SupplierInvoiceNumber,
              DeliveryDate = transactionDocument.TransactionDate,
              DueDate = transactionDocument.TransactionDate,
              ExchangeRate = (double)transactionDocument.ExchangeRate,
              OrderDate = transactionDocument.TransactionDate,
              OrderNo = transactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));
  }
}