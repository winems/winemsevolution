using System.Data.Entity;
using WineMS.Common.DataAccess;

namespace WineMS.WineMS.DataAccess {

  public class WineMsDbContext : DbContext {

    public DbSet<WineMsTransaction> WineMsTransactions { get; set; }
    
    public WineMsDbContext() : base(DatabaseConstants.WineMsConnectionStringName) { }

  }

}