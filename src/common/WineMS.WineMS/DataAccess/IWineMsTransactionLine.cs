namespace WineMS.WineMS.DataAccess {

  public interface IWineMsTransactionLine : IWineMsBufferEntry {

    string CurrencyCode { get; set; }
    string Description1 { get; set; }
    string GeneralLedgerAccountCode { get; set; }
    decimal Quantity { get; set; }
    byte TaxTypeId { get; set; }
    decimal TransactionAmountExVat { get; set; }

  }

}