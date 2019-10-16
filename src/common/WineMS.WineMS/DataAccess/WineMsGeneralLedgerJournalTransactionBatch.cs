using System;
using System.Collections.Generic;

namespace WineMS.WineMS.DataAccess {

  public class WineMsGeneralLedgerJournalTransactionBatch : WineMsTransaction {

    public string DocumentNumber { get; set; }

    public string TransactionType { get; set; }

    public WineMsGeneralLedgerJournalTransaction[] Transactions { get; set; }

    public List<GeneralLedgerJournalTransactionMapping> GeneralLedgerJournalTransactionMappings { get; } = new List<GeneralLedgerJournalTransactionMapping>();

    public void AddMapping(GeneralLedgerJournalTransactionMapping mapping) {
      GeneralLedgerJournalTransactionMappings.Add(mapping);
    }

  }

  public class GeneralLedgerJournalTransactionMapping {

    public string AuditNumber { get; set; }

    public Guid Guid { get; set; }

  }

}