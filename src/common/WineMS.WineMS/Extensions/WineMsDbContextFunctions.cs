using System;
using WineMS.WineMS.DataAccess;

namespace WineMS.WineMS.Extensions {

  public static class WineMsDbContextFunctions {

    public static void WrapInDbContext(Action<WineMsDbContext> action) {
      using (var context = new WineMsDbContext()) action(context);
    }

    public static T WrapInDbContext<T>(Func<WineMsDbContext, T> func) {
      using (var context = new WineMsDbContext()) return func(context);
    }

  }

}