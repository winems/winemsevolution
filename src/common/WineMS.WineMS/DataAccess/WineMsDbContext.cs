using System.Data.Entity;
using WineMS.Common.DataAccess;

namespace WineMS.WineMS.DataAccess {

  public class WineMsDbContext : DbContext {

    public DbSet<WineMsTransaction> WineMsTransactions { get; set; }
    
    public WineMsDbContext() : base(DatabaseConstants.WineMsConnectionStringName) { }

    public WineMsTransaction[] ListNewPurchaseOrders()
    {
      var t = 1;

      /*
       *
       */
      return new WineMsTransaction[] { };
    }

    public WineMsTransaction[] ListNewPurchaseStockReceipts()
    {
      var t = 1;

      /*
       *
       */

      return new WineMsTransaction[] { };
    }

  }

}