using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineMS.WineMS.DataAccess {

  [Table("WineMS Buffer Entry")]
  public class WineMsBufferEntry : IWineMsBufferEntry {

    [Column("Company")]
    public string CompanyId { get; set; }

    [Column("Completely Invoiced")]
    public byte CompletelyInvoiced { get; set; }

    [Key]
    [Column("GUID")]
    public Guid Guid { get; set; }

    [Column("OriginalGUID")]
    public Guid OriginalGuid { get; set; }

    [Column("Posted")]
    public byte PostedToAccountingSystem { get; set; }

  }

}