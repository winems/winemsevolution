using WineMS.WineMS.DataAccess;
using Xunit;

namespace WineMS.UnitTests.DataAccess {

  public class TestConnectWineMsDbContext {

    [Fact]
    public void CanConnectToDatabase()
    {
      var ex =
        Record
          .Exception(
            () =>
            {
              var context = new WineMsDbContext();
            });

      Assert.Null(ex);
    }

  }

}