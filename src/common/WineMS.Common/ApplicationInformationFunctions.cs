using RadiusCSharp.Core.AssemblyTools;
using RadiusCSharp.Core.Logging;
using WineMS.Common.DataAccess;

namespace WineMS.Common {

  public static class ApplicationInformationFunctions {

    public static (string Version, string WineMsDatabase, string EvolutionDatabase)
      GetApplicationInformation() =>
      (
        Version: AssemblyVersionFunctions.MajorMinor(),
        WineMsDatabase:
        $"| WineMS:{DatabaseConnectionStringFunctions.GetWineMsConnectionString().GetDatabaseDescriptor()}",
        EvolutionDatabase:
        $"| Evolution:{DatabaseConnectionStringFunctions.GetEvolutionCompanyConnectionString().GetDatabaseDescriptor()} " +
        $"| EvolutionCommon:{DatabaseConnectionStringFunctions.GetEvolutionCommonConnectionString().GetDatabaseDescriptor()}");

    public static void LogApplicationInformation(
      (string version, string wineMsDatabase, string evolutionDatabase) information)
    {
      $"GUI started: {information.version}".LogInfo();
      $"WineMS DB: {information.wineMsDatabase}".LogInfo();
      $"Evolution DB: {information.evolutionDatabase}".LogInfo();
    }

  }

}