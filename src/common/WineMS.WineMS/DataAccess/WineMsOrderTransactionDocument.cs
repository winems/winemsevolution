using System;

namespace WineMS.WineMS.DataAccess {

  public abstract class WineMsOrderTransactionDocument: WineMsTransaction, IWineMsTransactionDocument {

    public string DocumentNumber { get; set; }

    public decimal ExchangeRate { get; set; }

    public string IntegrationDocumentNumber { get; set; }

    public DateTime TransactionDate { get; set; }

    public IWineMsTransactionLine[] TransactionLines { get; set; }

    public string TransactionType { get; set; }
  }
}