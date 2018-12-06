using System.Linq;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.SalesOrders {

  public static class EvolutionJournalTransactionFunctions {

    public static Result<WineMsJournalTransactionBatch> ProcessTransaction(
      WineMsJournalTransactionBatch wineMsJournalTransactionBatch)
    {
      var i = 1;
      // TODO: ad logic to create and post GL journal.

      var journalTransactions = 
          wineMsJournalTransactionBatch
        .Transactions
        .Select(NewGeneralJournalTransactions)
        .ToArray()
        ;
      
      return Result.Ok(wineMsJournalTransactionBatch);
    }

    private static (GLTransaction debit, GLTransaction credit) NewGeneralJournalTransactions(WineMsJournalTransaction wineMsJournalTransaction) => 
      (new GLTransaction(), new GLTransaction());

  }

}