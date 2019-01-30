using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineMS.WineMS.DataAccess {

  [Table("_IntegrationCreditNoteBuffer")]
  public class WineMsCreditNoteTransaction : IWineMsTransactionLine {

    [Column("Company")]
    public string CompanyId { get; set; }

    [Column("Completely Invoiced")]
    public byte CompletelyInvoiced { get; set; }

    public string CurrencyCode { get; set; }

    [Column("CustomerAccountCode")]
    public string CustomerAccountCode { get; set; }

    [Column("Description 1")]
    public string Description1 { get; set; }

    public decimal DocumentDiscountPercentage { get; set; }

    [Column("Document No_")]
    public string DocumentNumber { get; set; }

    public decimal ExchangeRate { get; set; }

    [Column("GeneralLedgerItemCode")]
    public string GeneralLedgerItemCode { get; set; }

    [Key]
    [Column("GUID")]
    public Guid Guid { get; set; }

    public string ItemNote { get; set; }

    public decimal LineDiscountPercentage { get; set; }

    public string LineType { get; set; }
    
    public string MessageLine1 { get; set; }

    public string MessageLine2 { get; set; }

    public string MessageLine3 { get; set; }

    [Column("OriginalGUID")]
    public Guid OriginalGuid { get; set; }

    [Column("Posted")]
    public byte PostedToAccountingSystem { get; set; }

    [Column("Posting Date")]
    public DateTime PostingDate { get; set; }

    [Column("Quantity")]
    public decimal Quantity { get; set; }

    [Column("Item No_")]
    public string StockItemCode { get; set; }

    [Column("VAT Indicator")]
    public byte TaxTypeId { get; set; }

    [Column("LineTotalAmountExVat")]
    public decimal TransactionAmountExVat { get; set; }

    [Column("LineTotalAmountInVat")]
    public decimal TransactionAmountInVat { get; set; }

    [Column("Document Date")]
    public DateTime TransactionDate { get; set; }

    [Column("Transaction Type")]
    public string TransactionType { get; set; }

    [Column("Location Code")]
    public string WarehouseCode { get; set; }

  }

}