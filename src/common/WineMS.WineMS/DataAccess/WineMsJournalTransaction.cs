using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineMS.WineMS.DataAccess {

  [Table("_IntegrationJournalBuffer")]
  public class WineMsJournalTransaction: IWineMsBufferEntry {

    [Column("Company")]
    public string CompanyId { get; set; }

    public string CreditGeneralLedgerAccountCode { get; set; }

    public string DebitGeneralLedgerAccountCode { get; set; }

    [Column("Description 1")]
    public string Description1 { get; set; }

    [Column("Document No_")]
    public string DocumentNumber { get; set; }

    [Key]
    [Column("GUID")]
    public Guid Guid { get; set; }

    [Column("Posted")]
    public byte PostedToAccountingSystem { get; set; }

    [Column("Posting Date")]
    public DateTime PostingDate { get; set; }

    [Column("VAT Indicator")]
    public byte TaxTypeId { get; set; }

    [Column("LineTotalAmountExVat")]
    public decimal TransactionAmountExVat { get; set; }

    [Column("Document Date")]
    public DateTime TransactionDate { get; set; }

    [Column("Transaction Type")]
    public string TransactionType { get; set; }

  }

}