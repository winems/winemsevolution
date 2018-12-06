using System;
using RadiusCSharp.Core.DataAccess;

namespace WineMS.Common.Configuration {

  public static class SystemConfiguration {

    public static EvolutionConnectionStrings GetEvolutionConnectionStrings(this string companyId)
    {
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

  }

}