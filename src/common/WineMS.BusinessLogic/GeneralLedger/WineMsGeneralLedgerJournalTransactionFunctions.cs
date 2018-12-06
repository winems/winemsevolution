using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.SalesOrders;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.GeneralLedger {

  public static class WineMsGeneralLedgerJournalTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsGeneralLedgerJournalTransactions(),
          journalTransactionBatch =>
            EvolutionGeneralLedgerJournalTransactionFunctions
              .ProcessTransaction(journalTransactionBatch)
              .OnSuccess(
                transactionBatch => { transactionBatch.CompletePosting(IntegrationDocumentTypes.Journal); }));

  }

}