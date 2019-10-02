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
      WineMsPurchaseOrderTransactionDocument wineMsSalesOrderTransactionDocument,
      PurchaseOrderIntegrationType purchaseOrderIntegrationType) =>
      CreatePurchaseOrder(wineMsSalesOrderTransactionDocument)
        .Bind(
          order => order.AddPurchaseOrderLines(wineMsSalesOrderTransactionDocument))
        .Bind(
          order => ExceptionWrapper
            .Wrap(
              () => {
                switch (purchaseOrderIntegrationType) {
                  case PurchaseOrderIntegrationType.PurchaseOrder:
                    order.Save();
                    break;
                  case PurchaseOrderIntegrationType.GoodsReceivedVoucher:
                    ((PurchaseOrder) order).CompleteStock();
                    break;
                  case PurchaseOrderIntegrationType.SupplierInvoice:
                    order.Complete();
                    break;
                  default:
                    throw new ArgumentOutOfRangeException();
                }

                wineMsSalesOrderTransactionDocument.IntegrationDocumentNumber = order.OrderNo;
                return Result.Ok(wineMsSalesOrderTransactionDocument);
              }));

    private static Result<PurchaseOrder> CreatePurchaseOrder(
      WineMsPurchaseOrderTransactionDocument salesOrderTransactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new PurchaseOrder {
              Supplier = new Supplier(salesOrderTransactionDocument.SupplierAccountCode),
              DeliveryDate = salesOrderTransactionDocument.TransactionDate,
              DueDate = salesOrderTransactionDocument.TransactionDate,
              OrderDate = salesOrderTransactionDocument.TransactionDate,
              OrderNo = salesOrderTransactionDocument.DocumentNumber,
              TaxMode = TaxMode.Exclusive
            }));

  }

}