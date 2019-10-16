using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.GeneralLedger;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.GeneralLedger {

  public static class WineMsGeneralLedgerJournalTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsGeneralLedgerJournalTransactions(),
          journalTransactionBatch =>
            EvolutionGeneralLedgerJournalTransactionFunctions
              .ProcessTransaction((WineMsGeneralLedgerJournalTransactionBatch) journalTransactionBatch)
              .Tap(
                transactionBatch => { transactionBatch.CompletePosting(IntegrationDocumentTypes.Journal); }));

  }

}