namespace WineMS.WineMS.DataAccess {

  public class WineMsPurchaseOrderTransactionDocument: WineMsOrderTransactionDocument {

    public string SupplierAccountCode { get; set; }

    public string SupplierInvoiceNumber { get; set; }
  }
}