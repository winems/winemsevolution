using WineMS.WineMS.Extensions;
using Xunit;

namespace WineMS.UnitTests.DataAccess {

  public class TestWineMsDbContextWrapper {

    [Fact]
    public void ConnectToDatabase()
    {
      var ex =
        Record
          .Exception(
            () => { WineMsDbContextFunctions.WrapInDbContext(context => { }); });

      Assert.Null(ex);
    }

  }

}