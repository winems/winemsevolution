namespace WineMS.WineMS.DataAccess {

  public class WineMsJournalTransactionBatch : WineMsTransaction {

    public string DocumentNumber { get; set; }

    public string IntegrationDocumentNumber { get; set; }

    public string TransactionType { get; set; }

    public WineMsJournalTransaction[] Transactions { get; set; }

  }

}