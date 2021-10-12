namespace WineMS.WineMS.DataAccess {

  public class WineMsReturnToSupplierTransactionDocument: WineMsOrderTransactionDocument {

    public string SupplierAccountCode { get; set; }

    public string SupplierInvoiceNumber { get; set; }
  }
}