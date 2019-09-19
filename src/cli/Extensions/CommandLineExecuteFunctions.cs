using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using RadiusCSharp.Core.Logging;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;

namespace WineMsEvolutionCli.Extensions {

  public static class CommandLineExecuteFunctions {

    private const string CreditNotesCommand = "-creditnotes";
    private const string GeneralLedgerCommand = "-generalledger";
    private const string PurchaseOrdersCommand = "-purchaseorders";
    private const string ReturnToSupplierCommand = "-returntosupplier";
    private const string SalesOrdersCommand = "-salesorders";

    public static void Execute(string[] args) {
      args
        .ParseCommandLine()
        .Tap(
          commands => {
            var w = new NullBackgroundWorker();
            var _ =
              commands
                .Select(
                  a => {
                    $"Executing command: {a.Name}".LogInfo();
                    return a.CommandFunc.Invoke(w);
                  })
                .ToArray();
          })
        .OnFailure(err => err.LogException());
    }

    private static Result<IEnumerable<Command>> ParseCommandLine(this string[] args) {
      var commandLineOptions = new[] {
        GeneralLedgerCommand,
        SalesOrdersCommand,
        CreditNotesCommand,
        PurchaseOrdersCommand,
        ReturnToSupplierCommand
      };

      if (args == null || args.Length == 0)
        return Result.Fail<IEnumerable<Command>>($"No command line parameters specified. {HelpMessage()}");

      var commands = new List<Command>();

      foreach (var command in args)
        switch (command) {
          case GeneralLedgerCommand:
            commands.Add(new Command(GeneralLedgerCommand, WineMsTransactionFunctions.ProcessGeneralLedgerTransactions));
            break;
          case SalesOrdersCommand:
            commands.Add(new Command(SalesOrdersCommand, WineMsTransactionFunctions.ProcessSalesOrderTransactions));
            break;
          case CreditNotesCommand:
            commands.Add(new Command(CreditNotesCommand, WineMsTransactionFunctions.ProcessCreditNoteTransactions));
            break;
          case PurchaseOrdersCommand:
            commands.Add(new Command(PurchaseOrdersCommand, WineMsTransactionFunctions.ProcessPurchaseOrderTransactions));
            break;
          case ReturnToSupplierCommand:
            commands.Add(new Command(ReturnToSupplierCommand, WineMsTransactionFunctions.ProcessReturnToSupplierTransactions));
            break;
          default:
            return Result.Fail<IEnumerable<Command>>($"Command '{command}' is not valid. {HelpMessage()}");
        }

      return Result.Ok<IEnumerable<Command>>(commands);

      string HelpMessage() =>
        "Please specify one or more of the following commands:\r\n" +
        $"{string.Join("\r\n", commandLineOptions)}\r\n\r\n" +
        "E.g.:\r\n" +
        $"   WineMsEvolutionCli.exe {GeneralLedgerCommand}\r\n" +
        $"   WineMsEvolutionCli.exe {GeneralLedgerCommand} {SalesOrdersCommand}";
    }

    private class Command {

      public string Name { get; }
      public Func<IBackgroundWorker, Result> CommandFunc { get; }

      public Command(string name, Func<IBackgroundWorker, Result> commandFunc) {
        Name = name;
        CommandFunc = commandFunc;
      }

    }

  }

}