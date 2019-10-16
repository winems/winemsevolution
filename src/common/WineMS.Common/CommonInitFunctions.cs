using RadiusCSharp.Core.AppSettings;
using RadiusCSharp.Core.DataAccess;
using WineMS.Common.DataAccess;

namespace WineMS.Common {

  public static class CommonInitFunctions {

    public static void Init() {
      KeyValueFunctions.KeyValuesTableName = "IntegrationOptions";
      DataAccessConnectionFunctions.SetConnectionStringProvider(
        () =>
          DatabaseConstants.WineMsConnectionStringName.GetConnectionString());
    }

  }

}