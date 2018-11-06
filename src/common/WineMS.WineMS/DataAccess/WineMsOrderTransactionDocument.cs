using System;

namespace WineMS.WineMS.DataAccess {

  public abstract class WineMsOrderTransactionDocument : IWineMsTransactionDocument {

    public int BranchId { get; set; }
    public string CompanyId { get; set; }
    public string DocumentNumber { get; set; }
    public string IntegrationDocumentNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    public IWineMsTransactionLine[] TransactionLines { get; set; }
    public string TransactionType { get; set; }

  }

}