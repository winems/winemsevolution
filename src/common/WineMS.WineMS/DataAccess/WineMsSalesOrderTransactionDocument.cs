namespace WineMS.WineMS.DataAccess {

  public class WineMsSalesOrderTransactionDocument : WineMsOrderTransactionDocument {

    public string CustomerAccountCode { get; set; }
    public decimal DocumentDiscountPercentage { get; set; }
  }

}