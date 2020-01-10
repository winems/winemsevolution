namespace WineMS.WineMS.DataAccess {

  public interface IWineMsTransactionLine : IWineMsBufferEntry {

    string CurrencyCode { get; }
    string Description1 { get; }
    string GeneralLedgerItemCode { get; }
    string ItemNote { get; }
    decimal LineDiscountPercentage { get; }
    string LineType { get; }
    decimal Quantity { get; }
    byte TaxTypeId { get; }
    decimal TransactionAmountExVat { get; }
    decimal TransactionAmountInVat { get; }
    string WarehouseCode { get; }

  }

}