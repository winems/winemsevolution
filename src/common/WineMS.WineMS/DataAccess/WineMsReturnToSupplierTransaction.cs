using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineMS.WineMS.DataAccess {

  [Table("_IntegrationReturnToSupplierBuffer")]
  public class WineMsReturnToSupplierTransaction: IWineMsTransactionLine {

    [Column("Company")]
    public string CompanyId { get; set; }

    [Column("Completely Invoiced")]
    public byte CompletelyInvoiced { get; set; }

    public string CurrencyCode { get; set; }

    [Column("Description 1")]
    public string Description1 { get; set; }

    [Column("Document No_")]
    public string DocumentNumber { get; set; }

    public decimal ExchangeRate { get; set; }

    [Column("GeneralLedgerAccountCode")]
    public string GeneralLedgerItemCode { get; set; }

    [Key]
    [Column("GUID")]
    public Guid Guid { get; set; }

    public string ItemNote { get; set; }

    [NotMapped]
    public decimal LineDiscountPercentage { get; set; }

    public string LineType { get; set; }

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

    [Column("SupplierAccountCode")]
    public string SupplierAccountCode { get; set; }

    [Column("SupplierInvoice")]
    public string SupplierInvoiceNumber { get; set; }

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