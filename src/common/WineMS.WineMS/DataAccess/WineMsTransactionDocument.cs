using System;

namespace WineMS.WineMS.DataAccess {

  public struct WineMsTransactionDocument {

    public int BranchId { get; set; }
    public string CompanyId { get; set; }
    public string CustomerSupplierAccountCode { get; set; }
    public string DocumentNumber { get; set; }
    public string IntegrationDocumentNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    public WineMsTransaction[] TransactionLines { get; set; }
    public string TransactionType { get; set; }

  }

}