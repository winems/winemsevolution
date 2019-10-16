using System;
using System.Collections.Generic;

namespace WineMS.WineMS.DataAccess {

  public class WineMsStockJournalTransactionBatch : WineMsTransaction {

    public WineMsStockJournalTransaction[] Transactions { get; set; }

    public List<StockJournalTransactionMapping> StockJournalTransactionMappings { get; } = new List<StockJournalTransactionMapping>();

    public void AddMapping(StockJournalTransactionMapping mapping) {
      StockJournalTransactionMappings.Add(mapping);
    }

  }

  public class StockJournalTransactionMapping {

    public string AuditNumber { get; set; }

    public Guid Guid { get; set; }

  }

}