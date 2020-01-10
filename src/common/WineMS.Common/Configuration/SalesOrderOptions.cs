using WineMS.Common.Constants;

namespace WineMS.Common.Configuration {

  public class SalesOrderOptions {

    public SalesOrderIntegrationType IntegrationType { get; }
    public bool UseEvolutionInvoiceNumber { get; }

    public SalesOrderOptions(SalesOrderIntegrationType integrationType, bool useEvolutionInvoiceNumber) {
      IntegrationType = integrationType;
      UseEvolutionInvoiceNumber = useEvolutionInvoiceNumber;
    }

  }

}