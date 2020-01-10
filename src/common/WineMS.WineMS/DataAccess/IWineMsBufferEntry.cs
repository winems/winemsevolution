using System;

namespace WineMS.WineMS.DataAccess {

  public interface IWineMsBufferEntry {

    string CompanyId { get; }
    Guid Guid { get; }
    byte PostedToAccountingSystem { get; set; }

  }

}