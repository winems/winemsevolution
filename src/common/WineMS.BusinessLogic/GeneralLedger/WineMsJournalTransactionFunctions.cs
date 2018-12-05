using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.SalesOrders;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.GeneralLedger {

  public static class WineMsJournalTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsJournalTransactions(),
          journalTransactionBatch =>
            EvolutionJournalTransactionFunctions
              .ProcessTransaction(journalTransactionBatch)
              .OnSuccess(
                transactionBatch => { transactionBatch.CompletePosting(IntegrationDocumentTypes.Journal); }));

  }

}