using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Configuration;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.GeneralLedger {

  public static class EvolutionGeneralLedgerJournalTransactionFunctions {

    public static Result<WineMsGeneralLedgerJournalTransactionBatch> ProcessTransaction(
      WineMsGeneralLedgerJournalTransactionBatch batch) {
      batch
        .Transactions
        .Select(NewGeneralJournalTransactions)
        .ForEach(
          journalPair => {
            Post(journalPair.debit);
            Post(journalPair.credit);
          });

      return Result.Ok(batch);

      void Post(GeneralLedgerTransaction generalLedgerTransaction) {
        generalLedgerTransaction.Transaction.Post();
        batch.AddMapping(NewMapping(generalLedgerTransaction));
      }

      GeneralLedgerJournalTransactionMapping NewMapping(GeneralLedgerTransaction generalLedgerTransaction) =>
        new GeneralLedgerJournalTransactionMapping {
          Guid = generalLedgerTransaction.Guid,
          AuditNumber = generalLedgerTransaction.Transaction.Audit
        };
    }

    private static (GeneralLedgerTransaction debit, GeneralLedgerTransaction credit) NewGeneralJournalTransactions(
      WineMsGeneralLedgerJournalTransaction transaction) {
      var debit = NewGeneralJournalTransaction();
      debit.Transaction.Account = new GLAccount(transaction.DebitGeneralLedgerAccountCode);
      debit.Transaction.Debit = (double) transaction.TransactionAmountExVat;

      var credit = NewGeneralJournalTransaction();
      credit.Transaction.Account = new GLAccount(transaction.CreditGeneralLedgerAccountCode);
      credit.Transaction.Credit = (double) transaction.TransactionAmountExVat;

      return (debit, credit);

      GeneralLedgerTransaction NewGeneralJournalTransaction() =>
        new GeneralLedgerTransaction(
          transaction.Guid,
          new GLTransaction {
            Date = transaction.TransactionDate,
            Description = transaction.Description1,
            Reference = transaction.DocumentNumber,
            Reference2 = transaction.Guid.ToString(),
            TransactionCode = new TransactionCode(Module.GL, SystemConfiguration.GetJournalTransactionCode())
          });
    }

    private static void ForEach(
      this IEnumerable<(GeneralLedgerTransaction debit, GeneralLedgerTransaction credit)> journals,
      Action<(GeneralLedgerTransaction debit, GeneralLedgerTransaction credit)> action) {
      foreach (var journalPair in journals)
        action(journalPair);
    }

    private struct GeneralLedgerTransaction {

      public Guid Guid { get; }

      public GLTransaction Transaction { get; }

      public GeneralLedgerTransaction(Guid guid, GLTransaction transaction) {
        Guid = guid;
        Transaction = transaction;
      }

    }

  }

}