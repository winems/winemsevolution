using System;
using System.Linq;
using WineMS.WineMS.Extensions;
using Xunit;

namespace WineMS.UnitTests.DataAccess {

  public class TestListWineMsTransaction {

    [Fact]
    public void CanConnectToDatabase()
    {
      var result =
        WineMsDbContextFunctions
          .WrapInDbContext(
            context =>
              context
                .WineMsTransactions
                .FirstOrDefault(
                  a => a.Guid == new Guid("7a18f261-0673-43aa-b17a-50ecda5f2717")));

      Assert.NotNull(result);
      Assert.Equal(result.Guid, new Guid("7a18f261-0673-43aa-b17a-50ecda5f2717"));
    }

  }

}