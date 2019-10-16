using System;

namespace WineMS.WineMS.DataAccess {

  public class WineMsStockJournalTransactionDocument : WineMsTransaction {

    public string Description { get; set; }
    public double Quantity { get; set; }
    public string ReferenceNumber { get; set; }
    public string StockItemCode { get; set; }
    public string TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    public double UnitCostExVat { get; set; }
    public string WarehouseCode { get; set; }

  }

}