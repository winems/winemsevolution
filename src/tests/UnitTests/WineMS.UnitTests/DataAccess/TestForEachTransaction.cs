using System;
using WineMS.BusinessLogic.Extensions;
using Xunit;

namespace WineMS.UnitTests.DataAccess {

  public class TestCalcPercentProgress : IDisposable {

    public void Dispose() { }

    [Theory]
    [InlineData(1, 20, 5)]
    [InlineData(2, 20, 10)]
    [InlineData(3, 20, 15)]
    public void TestProgress(int count, int maxCount, int expected)
    {
      Assert.Equal(expected, ProgressReportFunctions.CalcPercentProgress(count, maxCount));
    }

  }

}