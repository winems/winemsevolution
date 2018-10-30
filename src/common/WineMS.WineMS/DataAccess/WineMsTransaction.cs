using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineMS.WineMS.DataAccess {

  [Table("WineMS Buffer Entry")]
  public class WineMsTransaction {

    [Column("Account")]
    public string AccountCode { get; set; }

    [Column("Company")]
    public string CompanyId { get; set; }

    [Column("Completely Invoiced")]
    public byte CompletelyInvoiced { get; set; }

    [Column("Balancing Account")]
    public string ContraAccountCode { get; set; }

    public string CurrencyCode { get; set; }

    [Column("Customer_Vendor")]
    public string CustomerSupplierAccountCode { get; set; }

    [Column("Description 1")]
    public string Description1 { get; set; }

    [Column("Document No_")]
    public string DocumentNumber { get; set; }

    [Key]
    [Column("GUID")]
    public Guid Guid { get; set; }

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

    [Column("Amount")]
    public decimal TransactionAmountExVat { get; set; }

    [Column("Document Date")]
    public DateTime TransactionDate { get; set; }

    [Column("Transaction Type")]
    public string TransactionType { get; set; }

  }

}