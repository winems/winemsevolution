using System;

namespace WineMS.WineMS.DataAccess {

  public interface IWineMsBufferEntry {

    string CompanyId { get; set; }
    Guid Guid { get; set; }
    byte PostedToAccountingSystem { get; set; }

  }

}