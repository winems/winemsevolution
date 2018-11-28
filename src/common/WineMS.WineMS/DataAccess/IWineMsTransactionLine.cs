namespace WineMS.WineMS.DataAccess {

  public interface IWineMsTransactionLine : IWineMsBufferEntry {

    string CurrencyCode { get; set; }
    string Description1 { get; set; }
    string GeneralLedgerItemCode { get; set; }
    decimal LineDiscountPercentage { get; set; }
    string LineType { get; set; }
    decimal Quantity { get; set; }
    byte TaxTypeId { get; set; }
    decimal TransactionAmountExVat { get; set; }
    string WarehouseCode { get; set; }

  }

}