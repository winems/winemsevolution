using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineMS.WineMS.DataAccess {

  [Table("IntegrationMappings")]
  public class IntegrationMapping {

    [Key]
    public int Id { get; set; }

    public string CompanyId { get; set; }

    public Guid Guid { get; set; }

    public string IntegrationDocumentNumber { get; set; }

    public string IntegrationDocumentType { get; set; }
    
  }

}