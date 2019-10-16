using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.Stock {

  public static class EvolutionStockJournalTransactionFunctions {

    public static Result<WineMsStockJournalTransactionBatch> ProcessTransaction(
      WineMsStockJournalTransactionBatch batch) {
      batch
        .Transactions
        .Select(NewStockTransaction)
        .ForEach(Post);

      return Result.Ok(batch);

      void Post(StockTransaction transaction) {
        var item = new InventoryItem(transaction.Transaction.StockItemCode);

        /***
         * Note
         *   Due to the way the Pastel SDK was designed we need to post a transaction
         *   to update the cost of the item before we post a quantity update.
         *
         *   The SDK ignores the cost given to it unless we are posting
         *   a InventoryOperation.CostAdjustment transaction.
         *
         */
        AdjustInventoryItemCost(item, transaction);

        var inventoryTransaction = InventoryAdjustmentTransaction(transaction.Transaction);
        inventoryTransaction.Quantity = (double) transaction.Transaction.Quantity;
        inventoryTransaction.Post();

        batch.AddMapping(NewMapping(transaction, inventoryTransaction.Audit));
      }

      StockJournalTransactionMapping NewMapping(StockTransaction transaction, string auditNumber) =>
        new StockJournalTransactionMapping {
          Guid = transaction.Guid,
          AuditNumber = auditNumber
        };
    }

    private static void AdjustInventoryItemCost(InventoryItem item, StockTransaction transaction) {
      var t = transaction.Transaction;
      if (!(Math.Abs(item.UnitCost - (double) t.UnitCostExVat) > 0.01)) return;

      var adjustCostTransaction = InventoryAdjustmentTransaction(t);

      adjustCostTransaction.UnitCost = (double) t.UnitCostExVat;
      adjustCostTransaction.Operation = InventoryOperation.CostAdjustment;
      adjustCostTransaction.Quantity = (double)t.Quantity;
      adjustCostTransaction.PostToGL = false;

      adjustCostTransaction.Post();
    }

    private static StockTransaction NewStockTransaction(WineMsStockJournalTransaction transaction) =>
      new StockTransaction(transaction.Guid, transaction);

    private static InventoryTransaction InventoryAdjustmentTransaction(WineMsStockJournalTransaction transaction) =>
      new InventoryTransaction {
        InventoryItem = new InventoryItem(transaction.StockItemCode),
        Description = transaction.Description,
        Date = transaction.TransactionDate,
        Reference = transaction.ReferenceNumber,
        TransactionCode = new TransactionCode(Module.Inventory, transaction.TransactionCode),
        Warehouse = string.IsNullOrWhiteSpace(transaction.WarehouseCode) ? null : new Warehouse(transaction.WarehouseCode)
      };

    private static void ForEach(this IEnumerable<StockTransaction> transactions, Action<StockTransaction> action) {
      foreach (var transaction in transactions)
        action(transaction);
    }

    private struct StockTransaction {

      public Guid Guid { get; }

      public WineMsStockJournalTransaction Transaction { get; }

      public StockTransaction(Guid guid, WineMsStockJournalTransaction transaction) {
        Guid = guid;
        Transaction = transaction;
      }

    }

  }

}