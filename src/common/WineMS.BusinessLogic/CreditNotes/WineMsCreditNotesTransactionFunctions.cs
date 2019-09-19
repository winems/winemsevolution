using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.CreditNotes;
using WineMS.WineMS.DataAccess;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.CreditNotes {

  public static class WineMsCreditNotesTransactionFunctions {

    public static Result Execute(IBackgroundWorker backgroundWorker) =>
      backgroundWorker
        .ForEachNewTransactionEvolutionContext(
          context => context.ListNewWineMsCreditNoteTransactions(),
          wineMsTransactionDocument =>
            EvolutionCreditNoteTransactionFunctions
              .ProcessTransaction((WineMsCreditNoteTransactionDocument) wineMsTransactionDocument)
              .OnSuccess(
                document => { document.CompletePosting(IntegrationDocumentTypes.CreditNote); }));

  }

}