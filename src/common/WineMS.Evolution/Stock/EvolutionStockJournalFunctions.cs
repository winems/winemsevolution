﻿using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Extensions;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.Stock {

  public static class EvolutionStockJournalFunctions {

    public static Result<WineMsStockJournalTransactionDocument> ProcessTransaction(
      WineMsStockJournalTransactionDocument stockJournalTransaction) =>
      CreateStockJournal(stockJournalTransaction)
        .Bind(
          transaction => ExceptionWrapper
            .Wrap(
              () => { return Result.Ok(stockJournalTransaction); }));

    private static Result<InventoryTransaction> CreateStockJournal(
      WineMsStockJournalTransactionDocument journal) =>
      ExceptionWrapper
        .Wrap(
          () => Result.Ok(
            new InventoryTransaction {
              InventoryItem = new InventoryItem(journal.StockItemCode),
              Description = journal.Description,
              Date = journal.TransactionDate,
              Reference = journal.ReferenceNumber,
              TransactionCode = new TransactionCode(Module.Inventory, journal.TransactionCode),
              Quantity = journal.Quantity,
              UnitCost = journal.UnitCostExVat,
              Warehouse = new Warehouse(journal.WarehouseCode)
            }));

  }

}