using System.Data.SqlClient;
using RadiusCSharp.Core.AppSettings;

namespace WineMS.Common.DataAccess {

  public static class DatabaseConnectionStringFunctions {

    public static string GetWineMsConnectionString() =>
      DatabaseConstants.WineMsConnectionStringName.GetConnectionString();

    public static string GetEvolutionCompanyConnectionString() =>
      DatabaseConstants.EvolutionCompanyConnectionStringName.GetConnectionString();

    public static string GetEvolutionCommonConnectionString() =>
      DatabaseConstants.EvolutionCommonConnectionStringName.GetConnectionString();

    public static string GetDatabaseDescriptor(this string connectionString)
    {
      var cb = new SqlConnectionStringBuilder(connectionString);
      return $"{cb.DataSource}\\{cb.InitialCatalog}";
    }

  }

}