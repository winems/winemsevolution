using System;
using CSharpFunctionalExtensions;
using WineMS.WineMS.DataAccess;

namespace WineMS.WineMS.Extensions {

  public static class WineMsTransactionDocumentFunctions {

    public static Result ForEachTransactionDocumentLine(
      this WineMsTransactionDocument transactionDocument,
      Func<WineMsTransaction, Result> func)
    {
      var result = Result.Ok();
      foreach (var transactionLine in transactionDocument.TransactionLines) {
        result = func(transactionLine);
        if (result.IsFailure) return result;
      }
      return result;
    }
    
  }

}