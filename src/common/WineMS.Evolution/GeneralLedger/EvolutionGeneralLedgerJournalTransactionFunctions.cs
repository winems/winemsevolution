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
      WineMsGeneralLedgerJournalTransactionBatch wineMsGeneralLedgerJournalTransactionBatch)
    {
      wineMsGeneralLedgerJournalTransactionBatch
        .Transactions
        .Select(NewGeneralJournalTransactions)
        .ForEach(
          journalPair =>
          {
            Post(journalPair.debit);
            Post(journalPair.credit);
          });

      return Result.Ok(wineMsGeneralLedgerJournalTransactionBatch);

      void Post(GeneralLedgerTransaction generalLedgerTransaction)
      {
        generalLedgerTransaction.Transaction.Post();
        wineMsGeneralLedgerJournalTransactionBatch
          .AddMapping(NewMapping(generalLedgerTransaction));
      }

      GeneralLedgerJournalTransactionMapping NewMapping(GeneralLedgerTransaction generalLedgerTransaction) =>
        new GeneralLedgerJournalTransactionMapping {
          Guid = generalLedgerTransaction.Guid,
          AuditNumber = generalLedgerTransaction.Transaction.Audit
        };
    }

    private static (GeneralLedgerTransaction debit, GeneralLedgerTransaction credit) NewGeneralJournalTransactions(
      WineMsGeneralLedgerJournalTransaction wineMsGeneralLedgerJournalTransaction)
    {
      var debit = NewGeneralJournalTransaction();
      debit.Transaction.Account = new GLAccount(wineMsGeneralLedgerJournalTransaction.DebitGeneralLedgerAccountCode);
      debit.Transaction.Debit = (double) wineMsGeneralLedgerJournalTransaction.TransactionAmountExVat;

      var credit = NewGeneralJournalTransaction();
      credit.Transaction.Account = new GLAccount(wineMsGeneralLedgerJournalTransaction.CreditGeneralLedgerAccountCode);
      credit.Transaction.Credit = (double) wineMsGeneralLedgerJournalTransaction.TransactionAmountExVat;

      return (debit: debit, credit: credit);

      GeneralLedgerTransaction NewGeneralJournalTransaction() =>
        new GeneralLedgerTransaction(
          wineMsGeneralLedgerJournalTransaction.Guid,
          new GLTransaction {
            Date = wineMsGeneralLedgerJournalTransaction.TransactionDate,
            Description = wineMsGeneralLedgerJournalTransaction.Description1,
            Reference = wineMsGeneralLedgerJournalTransaction.DocumentNumber,
            Reference2 = wineMsGeneralLedgerJournalTransaction.Guid.ToString(),
            TransactionCode = new TransactionCode(Module.GL, SystemConfiguration.GetJournalTransactionCode())
          });
    }

    private static void ForEach(
      this IEnumerable<(GeneralLedgerTransaction debit, GeneralLedgerTransaction credit)> journals,
      Action<(GeneralLedgerTransaction debit, GeneralLedgerTransaction credit)> action)
    {
      foreach (var journalPair in journals)
        action(journalPair);
    }

    private struct GeneralLedgerTransaction {

      public Guid Guid { get; }

      public GLTransaction Transaction { get; }

      public GeneralLedgerTransaction(Guid guid, GLTransaction transaction)
      {
        Guid = guid;
        Transaction = transaction;
      }

    }

  }

}