namespace WineMS.WineMS.DataAccess {

  public interface IWineMsTransactionDocument {

    string IntegrationDocumentNumber { get; set; }
    IWineMsTransactionLine[] TransactionLines { get; set; }

  }

}