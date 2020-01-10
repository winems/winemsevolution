namespace WineMS.WineMS.DataAccess {

  public interface IWineMsTransactionDocument {

    string IntegrationDocumentNumber { get; }
    IWineMsTransactionLine[] TransactionLines { get; }

  }

}