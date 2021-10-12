namespace WineMS.WineMS.DataAccess {

  public abstract class WineMsTransaction {

    public int BranchId { get; set; }

    public string CompanyId { get; set; }
  }
}