namespace WineMS.WineMS.DataAccess {

  public struct IntegrationMappingDescriptor {

    public string IntegrationDocumentNumber;
    public string IntegrationDocumentType;
    public IWineMsBufferEntry[] TransactionLines;

  }

}