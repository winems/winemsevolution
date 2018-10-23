using System;
using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Configuration;

namespace WineMS.Evolution {

  public static class EvolutionSdkFunctions {

    public static void WrapInEvolutionSdk(
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

        func()
          .OnSuccess(() => { DatabaseContext.CommitTran(); })
          .OnFailure(() => { DatabaseContext.RollbackTran(); });

      }
      catch {
        DatabaseContext.RollbackTran();
        throw;
      }
    }

  }

}