using CSharpFunctionalExtensions;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;
using WineMS.Common.Constants;
using WineMS.Evolution.SalesOrders;
using WineMS.WineMS.Extensions;

namespace WineMS.BusinessLogic.SalesOrders {

  public static class WineMsSalesOrdersTransactionFunctions {

    public static void Execute(IBackgroundWorker backgroundWorker)
    {
      var transactionTypes = new[] {WineMsTransactionTypes.SalesOrders};

      transactionTypes
        .ForEachNewTransactionEvolutionContext(
          backgroundWorker,
          wineMsTransactionDocument =>
          {
            return 
              EvolutionSalesOrderTransactionFunctions
                .ProcessTransaction(wineMsTransactionDocument)
                .OnSuccess(
                  document =>
                  {
                    WineMsDbContextFunctions
                      .WrapInDbContext(
                        context =>
                        {
                          context.SetAsPosted(document);
                          context.AddIntegrationMappings(document, IntegrationDocumentTypes.SalesOrder);
                          context.SaveChanges();
                        });
                  });
            
          });
    }

  }

}