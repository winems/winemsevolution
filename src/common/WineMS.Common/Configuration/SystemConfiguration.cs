using System;
using RadiusCSharp.Core.DataAccess;
using WineMS.Common.Constants;

namespace WineMS.Common.Configuration {

  public static class SystemConfiguration {

    public static EvolutionConnectionStrings GetEvolutionConnectionStrings(this string companyId) {
      const string commonDatabase = "EvolutionCommonDatabase";
      const string companyDatabase = "EvolutionCompanyDatabase";

      var commonDatabaseKeyName = $"{commonDatabase}-{companyId}";
      var companyDatabaseKeyName = $"{companyDatabase}-{companyId}";

      if (!commonDatabaseKeyName.GetKeyNameExists())
        commonDatabaseKeyName = $"{commonDatabase}-default";

      if (!companyDatabaseKeyName.GetKeyNameExists())
        companyDatabaseKeyName = $"{companyDatabase}-default";

      if (!commonDatabaseKeyName.GetKeyNameExists())
        throw new Exception(
          "Evolution common database connection string not defined. " +
          "Either define a company specific connection string using the " +
          $"'{commonDatabase}-{companyId}' key, or define a default " +
          $"connection string using the '{commonDatabase}-default' key.");

      if (!companyDatabaseKeyName.GetKeyNameExists())
        throw new Exception(
          "Evolution company database connection string not defined. " +
          "Either define a company specific connection string using the " +
          $"'{companyDatabase}-{companyId}' key, or define a default " +
          $"connection string using the '{companyDatabase}-default' key.");

      return new EvolutionConnectionStrings {
        CommonDatabase = commonDatabaseKeyName.GetKeyValueAsString(),
        CompanyDatabase = companyDatabaseKeyName.GetKeyValueAsString()
      };
    }

    public static string GetJournalTransactionCode() => "journal-transaction-code".GetKeyValueAsString();

    public static PurchaseOrderIntegrationType PurchaseOrderIntegrationType() {
      var integrationType = GetPurchaseOrderIntegrationType();
      switch (integrationType) {
        case IntegrationDocumentTypes.PurchaseOrder:
          return Constants.PurchaseOrderIntegrationType.PurchaseOrder;
        case IntegrationDocumentTypes.GoodsReceiveVoucher:
          return Constants.PurchaseOrderIntegrationType.GoodsReceivedVoucher;
        case IntegrationDocumentTypes.SupplierInvoice:
          return Constants.PurchaseOrderIntegrationType.SupplierInvoice;
        default:
          throw new Exception($"Purchase order integration type '{integrationType}' not found.");
      }

      string GetPurchaseOrderIntegrationType() => "purchase-order-integration-type".GetKeyValueAsString();
    }

    public static SalesOrderOptions GetSalesOrderOptions() =>
      new SalesOrderOptions(
          SalesOrderIntegrationType(),
          "sales-order-use-evolution-invoice-number".GetKeyValueAsBool()
        );

    private static SalesOrderIntegrationType SalesOrderIntegrationType() {
      var integrationType = GetSalesOrderIntegrationType();
      switch (integrationType) {
        case IntegrationDocumentTypes.SalesOrder:
          return Constants.SalesOrderIntegrationType.SalesOrder;
        case IntegrationDocumentTypes.TaxInvoice:
          return Constants.SalesOrderIntegrationType.TaxInvoice;
        default:
          return Constants.SalesOrderIntegrationType.SalesOrder;
      }

      string GetSalesOrderIntegrationType() => "sales-order-integration-type".GetKeyValueAsString();
    }

  }

}