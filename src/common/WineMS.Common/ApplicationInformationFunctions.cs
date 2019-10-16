using RadiusCSharp.Core.AssemblyTools;
using RadiusCSharp.Core.Logging;
using WineMS.Common.DataAccess;

namespace WineMS.Common {

  public static class ApplicationInformationFunctions {

    public static (string Version, string WineMsDatabase)
      GetApplicationInformation() =>
      (
        Version: AssemblyVersionFunctions.MajorMinor(),
        WineMsDatabase:
        $"| WineMS:{DatabaseConnectionStringFunctions.GetWineMsConnectionString().GetDatabaseDescriptor()}");

    public static void LogApplicationInformation(
      (string version, string wineMsDatabase) information) {
      $"Application started: {information.version}".LogInfo();
      $"WineMS DB: {information.wineMsDatabase}".LogInfo();
    }

  }

}