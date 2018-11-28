using System;
using System.Linq;
using WineMS.WineMS.Extensions;
using Xunit;

namespace WineMS.UnitTests.DataAccess {

  public class TestListWineMsTransaction {

    [Fact]
    public void CanLoadSalesOrderTransactions()
    {
      var result =
        WineMsDbContextFunctions
          .WrapInDbContext(
            context =>
              context
                .WineMsSalesOrderTransactions
                .ToArray());

      Assert.NotNull(result);
      Assert.NotEmpty(result);

    }

    [Fact]
    public void CanLoadPurchaseOrderTransactions()
    {
      var result =
        WineMsDbContextFunctions
          .WrapInDbContext(
            context =>
              context
                .WineMsPurchaseOrderTransactions
                .ToArray());

      Assert.NotNull(result);
      Assert.NotEmpty(result);

    }

  }

}