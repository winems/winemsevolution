namespace WineMS.WineMS.DataAccess {

  public class WineMsCreditNoteTransactionDocument : WineMsOrderTransactionDocument {

    public string CustomerAccountCode { get; set; }

    public decimal DocumentDiscountPercentage { get; set; }

    public decimal ExchangeRate { get; set; }

    public string MessageLine1 { get; set; }

    public string MessageLine2 { get; set; }

    public string MessageLine3 { get; set; }

  }

}