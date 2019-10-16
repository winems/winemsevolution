using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineMS.WineMS.DataAccess {

  [Table("_IntegrationStockJournalBuffer")]
  public class WineMsStockJournalTransaction : IWineMsBufferEntry {

    [Column("Company")]
    public string CompanyId { get; set; }

    public string Description { get; set; }

    [Key]
    [Column("GUID")]
    public Guid Guid { get; set; }

    [Column("Posted")]
    public byte PostedToAccountingSystem { get; set; }

    [Column("Posting Date")]
    public DateTime PostingDate { get; set; }

    public double Quantity { get; set; }

    public string ReferenceNumber { get; set; }

    [Column("Document Date")]
    public DateTime TransactionDate { get; set; }

    [Column("Transaction Type")]
    public string TransactionType { get; set; }

    public string StockItemCode { get; set; }

    [Column("LineTotalAmountExVat")]
    public double UnitCostExVat { get; set; }

    public string WarehouseCode { get; set; }


  }

}