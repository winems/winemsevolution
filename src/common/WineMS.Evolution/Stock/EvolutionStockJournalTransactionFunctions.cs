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
        transaction.Transaction.Post();
        batch.AddMapping(NewMapping(transaction));
      }

      StockJournalTransactionMapping NewMapping(StockTransaction transaction) =>
        new StockJournalTransactionMapping {
          Guid = transaction.Guid,
          AuditNumber = transaction.Transaction.Audit
        };
    }

    private static StockTransaction NewStockTransaction(WineMsStockJournalTransaction transaction) =>
      new StockTransaction(
        transaction.Guid,
        new InventoryTransaction {
          InventoryItem = new InventoryItem(transaction.StockItemCode),
          Description = transaction.Description,
          Date = transaction.TransactionDate,
          Reference = transaction.ReferenceNumber,
          TransactionCode = new TransactionCode(Module.Inventory, transaction.TransactionType),
          Quantity = transaction.Quantity,
          UnitCost = transaction.UnitCostExVat,
          Warehouse = string.IsNullOrWhiteSpace(transaction.WarehouseCode) ? null : new Warehouse(transaction.WarehouseCode)
        });

    private static void ForEach(this IEnumerable<StockTransaction> transactions, Action<StockTransaction> action) {
      foreach (var transaction in transactions)
        action(transaction);
    }

    private struct StockTransaction {

      public Guid Guid { get; }

      public InventoryTransaction Transaction { get; }

      public StockTransaction(Guid guid, InventoryTransaction transaction) {
        Guid = guid;
        Transaction = transaction;
      }

    }

  }

}