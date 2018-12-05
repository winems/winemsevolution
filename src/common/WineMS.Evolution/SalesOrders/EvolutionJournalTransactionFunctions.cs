using CSharpFunctionalExtensions;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.SalesOrders {

  public static class EvolutionJournalTransactionFunctions {

    public static Result<WineMsJournalTransactionBatch> ProcessTransaction(
      WineMsJournalTransactionBatch wineMsJournalTransactionBatch) =>
      Result.Ok(wineMsJournalTransactionBatch);

  }

}