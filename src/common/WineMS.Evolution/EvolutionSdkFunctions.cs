using System;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using RadiusCSharp.Core.Logging;
using WineMS.Common.Configuration;

namespace WineMS.Evolution {

  public static class EvolutionSdkFunctions {

    public static Result WrapInEvolutionSdk(
      this EvolutionConnectionStrings connectionStrings,
      int branchId,
      Func<Result> func)
    {
      try {
        DatabaseContext.CreateCommonDBConnection(connectionStrings.CommonDatabase);
        DatabaseContext.SetLicense("DE09110064", "2428759");
        DatabaseContext.CreateConnection(connectionStrings.CompanyDatabase);
        DatabaseContext.BeginTran();
        if (branchId > 0)
          DatabaseContext.SetBranchContext(branchId);

        return func()
          .OnSuccess(() => { DatabaseContext.CommitTran(); })
          .OnFailure(() => { DatabaseContext.RollbackTran(); });
      }
      catch (Exception ex) {
        DatabaseContext.RollbackTran();
        return Result.Fail(ex.GetExceptionMessages());
      }
    }

  }

}